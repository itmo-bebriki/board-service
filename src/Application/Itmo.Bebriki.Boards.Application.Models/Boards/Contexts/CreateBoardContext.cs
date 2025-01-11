namespace Itmo.Bebriki.Boards.Application.Models.Boards.Contexts;

public sealed record CreateBoardContext(
    string Name,
    string Description,
    IReadOnlySet<long> TopicIds,
    DateTimeOffset CreatedAt);