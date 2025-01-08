namespace Itmo.Bebriki.Boards.Application.Models.Boards.Contexts;

public sealed record UpdateBoardContext(
    string Name,
    string Description,
    DateTimeOffset UpdatedAt);