namespace Itmo.Bebriki.Boards.Application.Contracts.Topics.Events;

public sealed record CreateTopicEvent(
    long TopicId,
    string Name,
    string Description,
    IReadOnlySet<long> TaskIds,
    DateTimeOffset CreatedAt);