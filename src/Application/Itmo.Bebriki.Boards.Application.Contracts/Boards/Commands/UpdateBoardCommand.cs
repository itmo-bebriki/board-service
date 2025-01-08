namespace Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;

public sealed record UpdateBoardCommand(
    long BoardId,
    string? Name = null,
    string? Description = null);