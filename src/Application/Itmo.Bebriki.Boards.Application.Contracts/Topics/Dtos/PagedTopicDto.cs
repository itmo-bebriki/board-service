namespace Itmo.Bebriki.Boards.Application.Contracts.Topics.Dtos;

public sealed record PagedTopicDto(long? Cursor, IReadOnlyCollection<TopicDto> TopicDtos);