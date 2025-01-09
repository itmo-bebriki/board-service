namespace Itmo.Bebriki.Boards.Application.Models.Boards.Contexts;

public sealed record UpdateBoardContext(
    long BoardId,
    string Name,
    string Description,
    DateTimeOffset UpdatedAt);