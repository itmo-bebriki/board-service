using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Boards;
using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Repositories.Boards;
using Itmo.Bebriki.Boards.Application.Models.Boards;
using Itmo.Dev.Platform.Persistence.Abstractions.Commands;
using Itmo.Dev.Platform.Persistence.Abstractions.Connections;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Itmo.Bebriki.Boards.Infrastructure.Persistence.Repositories;

internal sealed class BoardRepository : IBoardRepository
{
    private readonly IPersistenceConnectionProvider _connectionProvider;

    public BoardRepository(IPersistenceConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async IAsyncEnumerable<Board> QueryAsync(
        BoardQuery query,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const string sql =
        """
        select 
            b.board_id,
            b.name,
            b.description,
            coalesce(bt.topic_ids, '{}') as topic_ids,
            b.updated_at
        from boards as b
        left join (
            select 
                bt.board_id,
                array_agg(bt.topic_id) as topic_ids
            from board_topics as bt
            group by bt.board_id
        ) as bt 
            on b.board_id = bt.board_id
        where (:cursor is null or b.board_id > :cursor)
            and (cardinality(:board_ids) = 0 or b.board_id = any(:board_ids))
            and (:from_updated_at is null or b.updated_at >= :from_updated_at)
            and (:to_updated_at is null or b.updated_at <= :to_updated_at)
        order by b.board_id
        limit :page_size;
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("board_ids", query.BoardIds)
            .AddParameter("from_updated_at", query.FromUpdatedAt)
            .AddParameter("to_updated_at", query.ToUpdatedAt)
            .AddParameter("cursor", query.Cursor)
            .AddParameter("page_size", query.PageSize);

        await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            yield return BoardFactory.CreateNew(
                id: reader.GetInt64("board_id"),
                name: reader.GetString("name"),
                description: reader.GetString("description"),
                topicIds: reader.GetFieldValue<long[]>("topic_ids"),
                updatedAt: reader.GetFieldValue<DateTimeOffset>("updated_at"));
        }
    }

    public async IAsyncEnumerable<long> AddAsync(
        IReadOnlyCollection<Board> boards,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        const string insertBoardsSql =
        """
        insert into boards as b 
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
        returning b.board_id;
        """;

        const string insertBoardTopicsSql =
        """
        insert into board_topics as bt (board_id, topic_id)
        select :board_id, unnest(:topic_ids);
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand insertBoardsCommand = connection.CreateCommand(insertBoardsSql)
            .AddParameter("names", boards.Select(b => b.Name))
            .AddParameter("descriptions", boards.Select(b => b.Description))
            .AddParameter("updated_at", boards.Select(b => b.UpdatedAt));

        var newBoardIds = new List<long>();

        await using (DbDataReader reader = await insertBoardsCommand.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                newBoardIds.Add(reader.GetInt64(0));
            }
        }

        for (int i = 0; i < boards.Count; i++)
        {
            Board board = boards.ElementAt(i);

            if (board.TopicIds.Count == 0)
            {
                continue;
            }

            await using IPersistenceCommand insertBoardTopicsCommand = connection.CreateCommand(insertBoardTopicsSql)
                .AddParameter("board_id", newBoardIds[i])
                .AddParameter("topic_ids", board.TopicIds.ToArray());

            await insertBoardTopicsCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        foreach (long boardId in newBoardIds)
        {
            yield return boardId;
        }
    }

    public async Task UpdateAsync(IReadOnlyCollection<Board> boards, CancellationToken cancellationToken)
    {
        const string sql =
        """
        update boards as b
        set 
            name = source.name,
            description = source.description,
            updated_at = source.updated_at
        from unnest(
             :board_ids,
             :names,
             :descriptions,
             :updated_ats
        ) as source (
            board_id,
            name,
            description,
            updated_at
        )
        where b.board_id = source.board_id;
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("board_ids", boards.Select(b => b.Id))
            .AddParameter("names", boards.Select(b => b.Name))
            .AddParameter("descriptions", boards.Select(b => b.Description))
            .AddParameter("updated_ats", boards.Select(b => b.UpdatedAt));

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task AddTopicsAsync(BoardTopicsQuery query, CancellationToken cancellationToken)
    {
        const string sql =
        """
        insert into board_topics as bt (board_id, topic_id)
        select :board_id, unnest(:topic_ids);
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("board_id", query.BoardId)
            .AddParameter("topic_ids", query.TopicIds.ToArray());

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task RemoveTopicsAsync(BoardTopicsQuery query, CancellationToken cancellationToken)
    {
        const string sql =
        """
        delete from board_topics as bt
        where bt.board_id = :board_id
            or bt.topic_id = any(:topic_ids);
        """;

        await using IPersistenceConnection connection = await _connectionProvider.GetConnectionAsync(cancellationToken);

        await using IPersistenceCommand command = connection.CreateCommand(sql)
            .AddParameter("board_id", query.BoardId)
            .AddParameter("topic_ids", query.TopicIds.ToArray());

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}