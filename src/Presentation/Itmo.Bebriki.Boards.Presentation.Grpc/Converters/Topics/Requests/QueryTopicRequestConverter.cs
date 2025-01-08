using Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;
using Itmo.Bebriki.Topics.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Topics.Requests;

internal static class QueryTopicRequestConverter
{
    internal static QueryTopicCommand ToInternal(QueryTopicRequest request)
    {
        return new QueryTopicCommand(
            TopicIds: request.TopicIds?.ToArray() ?? [],
            FromUpdatedAt: request.FromUpdatedAt?.ToDateTimeOffset(),
            ToUpdatedAt: request.ToUpdatedAt?.ToDateTimeOffset(),
            Cursor: request.Cursor,
            PageSize: request.PageSize);
    }
}