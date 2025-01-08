namespace Itmo.Bebriki.Boards.Application.Contracts.Topics.Dtos;

public sealed record TopicDto(
    long Id,
    string Name,
    string Description,
    IReadOnlySet<long> TaskIds,
    DateTime UpdatedAt);