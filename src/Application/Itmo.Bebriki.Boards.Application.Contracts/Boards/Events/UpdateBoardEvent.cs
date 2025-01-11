using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Boards.Application.Contracts.Boards.Events;

public sealed record UpdateBoardEvent(
    long BoardId,
    DateTimeOffset UpdatedAt,
    string? Name = null,
    string? Description = null) : IEvent;