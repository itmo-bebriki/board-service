using Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;
using Itmo.Bebriki.Boards.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Boards.Requests;

internal static class UpdateBoardRequestConverter
{
    internal static UpdateBoardCommand ToInternal(UpdateBoardRequest request)
    {
        return new UpdateBoardCommand(
            BoardId: request.BoardId,
            Name: request.Name,
            Description: request.Description);
    }
}