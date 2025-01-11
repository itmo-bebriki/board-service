using Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;
using Itmo.Bebriki.Topics.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Topics.Requests;

internal static class UpdateTopicRequestConverter
{
    internal static UpdateTopicCommand ToInternal(UpdateTopicRequest request)
    {
        return new UpdateTopicCommand(
            TopicId: request.TopicId,
            Name: request.Name,
            Description: request.Description);
    }
}