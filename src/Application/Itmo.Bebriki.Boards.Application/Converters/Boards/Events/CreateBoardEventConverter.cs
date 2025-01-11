using Itmo.Bebriki.Boards.Application.Contracts.Boards.Events;
using Itmo.Bebriki.Boards.Application.Models.Boards;

namespace Itmo.Bebriki.Boards.Application.Converters.Boards.Events;

internal static class CreateBoardEventConverter
{
    internal static CreateBoardEvent ToEvent(long boardId, Board board)
    {
        return new CreateBoardEvent(
            BoardId: boardId,
            Name: board.Name,
            Description: board.Description,
            TopicIds: board.TopicIds,
            CreatedAt: board.UpdatedAt);
    }
}