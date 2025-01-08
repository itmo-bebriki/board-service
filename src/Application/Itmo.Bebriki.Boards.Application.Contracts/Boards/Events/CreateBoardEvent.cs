using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Boards.Application.Contracts.Boards.Events;

public sealed record CreateBoardEvent(
    long BoardId,
    string Name,
    string Description,
    IReadOnlySet<long> TopicIds,
    DateTimeOffset CreatedAt) : IEvent;