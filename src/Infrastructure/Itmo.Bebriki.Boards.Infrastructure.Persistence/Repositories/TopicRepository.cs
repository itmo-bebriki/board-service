using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Topics;
using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Repositories.Topics;
using Itmo.Bebriki.Boards.Application.Models.Topics;
using Itmo.Dev.Platform.Persistence.Abstractions.Commands;
using Itmo.Dev.Platform.Persistence.Abstractions.Connections;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Itmo.Bebriki.Boards.Infrastructure.Persistence.Repositories;

internal sealed class TopicRepository : ITopicRepository
{
    private readonly IPersistenceConnectionProvider _connectionProvider;

    public TopicRepository(IPersistenceConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async IAsyncEnumerable<Topic> QueryAsync(
        TopicQuery query,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const string sql =
        """
        select 
            t.topic_id,
            t.name,
            t.description,
            coalesce(tt.task_ids, '{}') as task_ids,
            t.updated_at
        from topics as t
        left join (
            select 
                tt.topic_id,
                array_agg(tt.task_id) as task_ids
            from topic_tasks as tt
            group by tt.topic_id
        ) as tt 
            on t.topic_id = tt.topic_id
        where (:cursor is null or t.topic_id > :cursor)
            and (cardinality(:topic_ids) = 0 or t.topic_id = any(:topic_ids))
            and (:from_updated_at is null or t.updated_at >= :from_updated_at)
            and (:to_updated_at is null or t.updated_at <= :to_updated_at)
        order by t.topic_id
        limit :page_size;
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("topic_ids", query.TopicIds)
            .AddParameter("from_updated_at", query.FromUpdatedAt)
            .AddParameter("to_updated_at", query.ToUpdatedAt)
            .AddParameter("cursor", query.Cursor)
            .AddParameter("page_size", query.PageSize);

        await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            yield return TopicFactory.CreateNew(
                id: reader.GetInt64("topic_id"),
                name: reader.GetString("name"),
                description: reader.GetString("description"),
                taskIds: reader.GetFieldValue<long[]>("task_ids"),
                updatedAt: reader.GetFieldValue<DateTimeOffset>("updated_at"));
        }
    }

    public async IAsyncEnumerable<long> AddAsync(
        IReadOnlyCollection<Topic> topics,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const string insertTopicsSql =
        """
        insert into topics as t 
            (name, description, updated_at)
        select
            source.name,
            source.description,
            source.updated_at
        from unnest(
             :names,
             :descriptions,
             :updated_ats
        ) as source (
            name,
            description,
            updated_at
        )
        returning t.topic_id;
        """;

        const string insertTopicTasksSql =
        """
        insert into topic_tasks as tt (topic_id, task_id)
        select :topic_id, unnest(:task_ids);
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand insertTopicsCommand = connection.CreateCommand(insertTopicsSql)
            .AddParameter("names", topics.Select(b => b.Name))
            .AddParameter("descriptions", topics.Select(b => b.Description))
            .AddParameter("updated_ats", topics.Select(b => b.UpdatedAt));

        var newTopicIds = new List<long>();

        await using (DbDataReader reader = await insertTopicsCommand.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                newTopicIds.Add(reader.GetInt64(0));
            }
        }

        for (int i = 0; i < topics.Count; i++)
        {
            Topic topic = topics.ElementAt(i);

            if (topic.TaskIds.Count == 0)
            {
                continue;
            }

            await using IPersistenceCommand insertTopicTasksCommand = connection.CreateCommand(insertTopicTasksSql)
                .AddParameter("topic_id", newTopicIds[i])
                .AddParameter("task_ids", topic.TaskIds.ToArray());

            await insertTopicTasksCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (long topicId in newTopicIds)
        {
            yield return topicId;
        }
    }

    public async Task UpdateAsync(IReadOnlyCollection<Topic> topics, CancellationToken cancellationToken)
    {
        const string sql =
        """
        update topics as t
        set 
            name = source.name,
            description = source.description,
            updated_at = source.updated_at
        from unnest(
             :topic_ids,
             :names,
             :descriptions,
             :updated_ats
        ) as source (
            topic_id,
            name,
            description,
            updated_at
        )
        where t.topic_id = source.topic_id;
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("topic_ids", topics.Select(b => b.Id))
            .AddParameter("names", topics.Select(b => b.Name))
            .AddParameter("descriptions", topics.Select(b => b.Description))
            .AddParameter("updated_ats", topics.Select(b => b.UpdatedAt));

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task AddTasksAsync(TopicTasksQuery query, CancellationToken cancellationToken)
    {
        const string sql =
        """
        insert into topic_tasks as tt (topic_id, task_id)
        select :topic_id, unnest(:task_ids);
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("topic_id", query.TopicId)
            .AddParameter("task_ids", query.TaskIds.ToArray());

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task RemoveTasksAsync(TopicTasksQuery query, CancellationToken cancellationToken)
    {
        const string sql =
        """
        delete from topic_tasks as tt
        where tt.topic_id = :topic_id
            and tt.task_id = any(:task_ids);
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("topic_id", query.TopicId)
            .AddParameter("task_ids", query.TaskIds.ToArray());

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}