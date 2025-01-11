using Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;
using Itmo.Bebriki.Boards.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Boards.Requests;

internal static class CreateBoardRequestConverter
{
    internal static CreateBoardCommand ToInternal(CreateBoardRequest request)
    {
        return new CreateBoardCommand(
            Name: request.Name,
            Description: request.Description,
            request.TopicIds.ToHashSet());
    }
}