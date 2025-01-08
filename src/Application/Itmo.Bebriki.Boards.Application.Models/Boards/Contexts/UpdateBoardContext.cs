namespace Itmo.Bebriki.Boards.Application.Models.Boards.Contexts;

public sealed record UpdateBoardContext(
    DateTimeOffset UpdatedAt,
    string? Name = null);