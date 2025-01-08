namespace Itmo.Bebriki.Boards.Application.Models.Topics.Contexts;

public sealed record CreateTopicContext(
    string Name,
    string Description,
    IReadOnlySet<long> TaskIds,
    DateTimeOffset CreatedAt);