namespace Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;

public sealed record QueryTopicCommand(
    long[] TaskIds,
    DateTimeOffset? FromUpdatedAt,
    DateTimeOffset? ToUpdatedAt,
    long? Cursor,
    int PageSize);