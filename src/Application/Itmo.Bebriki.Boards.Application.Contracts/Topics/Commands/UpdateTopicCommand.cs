namespace Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;

public sealed record UpdateTopicCommand(
    long TopicId,
    string? Name = null,
    string? Description = null);