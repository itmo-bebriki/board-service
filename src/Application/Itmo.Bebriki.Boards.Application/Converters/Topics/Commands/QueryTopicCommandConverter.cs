using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Topics;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;

namespace Itmo.Bebriki.Boards.Application.Converters.Topics.Commands;

internal static class QueryTopicCommandConverter
{
    internal static TopicQuery ToQuery(QueryTopicCommand command)
    {
        return TopicQuery.Build(builder => builder
            .WithTopicIds(command.TopicIds)
            .WithFromUpdatedAt(command.FromUpdatedAt)
            .WithToUpdatedAt(command.ToUpdatedAt)
            .WithCursor(command.Cursor)
            .WithPageSize(command.PageSize));
    }
}