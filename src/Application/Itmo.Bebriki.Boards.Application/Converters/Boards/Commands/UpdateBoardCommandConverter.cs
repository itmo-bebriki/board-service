using Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;
using Itmo.Bebriki.Boards.Application.Models.Boards;
using Itmo.Bebriki.Boards.Application.Models.Boards.Contexts;

namespace Itmo.Bebriki.Boards.Application.Converters.Boards.Commands;

internal static class UpdateBoardCommandConverter
{
    internal static UpdateBoardContext ToContext(
        UpdateBoardCommand command,
        Board prevBoard,
        DateTimeOffset updatedAt)
    {
        return new UpdateBoardContext(
            BoardId: prevBoard.Id,
            Name: command.Name ?? prevBoard.Name,
            Description: command.Description ?? prevBoard.Description,
            TopicIds: prevBoard.TopicIds,
            UpdatedAt: updatedAt);
    }
}