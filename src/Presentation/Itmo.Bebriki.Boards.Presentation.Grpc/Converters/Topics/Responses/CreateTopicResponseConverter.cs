using Itmo.Bebriki.Topics.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Topics.Responses;

internal static class CreateTopicResponseConverter
{
    internal static CreateTopicResponse FromInternal(long boardId)
    {
        return new CreateTopicResponse
        {
            TopicId = boardId,
        };
    }
}