namespace Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;

public sealed record SetBoardTopicsCommand(
    long BoardId,
    IReadOnlySet<long> TopicIds);