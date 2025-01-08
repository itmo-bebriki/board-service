namespace Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;

public sealed record SetTopicTasksCommand(
    long TopicId,
    IReadOnlySet<long> TaskIds);