namespace Itmo.Bebriki.Boards.Application.Contracts.Boards.Dtos;

public sealed record PagedBoardDto(long? Cursor, IReadOnlyCollection<BoardDto> BoardDtos);