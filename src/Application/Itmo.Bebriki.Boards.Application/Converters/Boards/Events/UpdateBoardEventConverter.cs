using Itmo.Bebriki.Boards.Application.Contracts.Boards.Events;
using Itmo.Bebriki.Boards.Application.Models.Boards;

namespace Itmo.Bebriki.Boards.Application.Converters.Boards.Events;

internal static class UpdateBoardEventConverter
{
    internal static UpdateBoardEvent ToEvent(Board prevBoard, Board updatedBoard)
    {
        return new UpdateBoardEvent(
            BoardId: updatedBoard.Id,
            UpdatedAt: updatedBoard.UpdatedAt,
            Name: prevBoard.Name == updatedBoard.Name ? null : updatedBoard.Name,
            Description: prevBoard.Description == updatedBoard.Description ? null : updatedBoard.Description);
    }
}