namespace Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;

public sealed record CreateTopicCommand(
    string Name,
    string Description,
    IReadOnlySet<long> TaskIds);