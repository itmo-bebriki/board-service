namespace Itmo.Bebriki.Boards.Application.Contracts.Boards.Dtos;

public sealed record BoardDto(
    long Id,
    string Name,
    string Description,
    IReadOnlySet<long> TopicIds,
    DateTimeOffset UpdatedAt);