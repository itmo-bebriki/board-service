using Itmo.Bebriki.Boards.Application.Contracts.Boards.Dtos;
using Itmo.Bebriki.Boards.Application.Models.Boards;

namespace Itmo.Bebriki.Boards.Application.Converters.Boards.Dtos;

internal static class BoardDtoConverter
{
    internal static BoardDto ToDto(Board board)
    {
        return new BoardDto(
            Id: board.Id,
            Name: board.Name,
            Description: board.Description,
            TopicIds: board.TopicIds,
            UpdatedAt: board.UpdatedAt);
    }
}