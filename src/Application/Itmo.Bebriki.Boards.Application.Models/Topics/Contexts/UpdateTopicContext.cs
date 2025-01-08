namespace Itmo.Bebriki.Boards.Application.Models.Topics.Contexts;

public sealed record UpdateTopicContext(
    string Name,
    string Description,
    DateTimeOffset UpdatedAt);