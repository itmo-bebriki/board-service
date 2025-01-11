namespace Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;

public sealed record CreateBoardCommand(
    string Name,
    string Description,
    IReadOnlySet<long> TopicIds);