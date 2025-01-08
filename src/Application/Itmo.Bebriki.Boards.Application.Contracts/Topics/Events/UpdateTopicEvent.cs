namespace Itmo.Bebriki.Boards.Application.Contracts.Topics.Events;

public sealed record UpdateTopicEvent(
    long TopicId,
    DateTimeOffset UpdatedAt,
    string? Name = null,
    string? Description = null);