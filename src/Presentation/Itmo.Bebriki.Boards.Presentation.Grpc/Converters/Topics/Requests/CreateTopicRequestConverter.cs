using Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;
using Itmo.Bebriki.Topics.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Topics.Requests;

internal static class CreateTopicRequestConverter
{
    internal static CreateTopicCommand ToInternal(CreateTopicRequest request)
    {
        return new CreateTopicCommand(
            Name: request.Name,
            Description: request.Description,
            request.TaskIds.ToHashSet());
    }
}