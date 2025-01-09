namespace Itmo.Bebriki.Boards.Application.Models.Topics.Contexts;

public sealed record UpdateTopicContext(
    long TopicId,
    string Name,
    string Description,
    IReadOnlySet<long> TaskIds,
    DateTimeOffset UpdatedAt);