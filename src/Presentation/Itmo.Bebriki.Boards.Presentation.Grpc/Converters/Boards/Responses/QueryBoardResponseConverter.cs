using Itmo.Bebriki.Boards.Application.Contracts.Boards.Dtos;
using Itmo.Bebriki.Boards.Contracts;
using Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Boards.Dtos;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Boards.Responses;

internal static class QueryBoardResponseConverter
{
    internal static QueryBoardResponse FromInternal(PagedBoardDto dto)
    {
        return new QueryBoardResponse
        {
            Cursor = dto.Cursor,
            Boards = { dto.BoardDtos.Select(BoardDtoConverter.FromInternal) },
        };
    }
}