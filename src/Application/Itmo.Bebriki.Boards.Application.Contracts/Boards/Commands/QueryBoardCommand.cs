namespace Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;

public sealed record QueryBoardCommand(
    long[] BoardIds,
    DateTimeOffset? FromUpdatedAt,
    DateTimeOffset? ToUpdatedAt,
    long? Cursor,
    int PageSize);