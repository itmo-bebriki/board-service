namespace Itmo.Bebriki.Boards.Application.Models.Boards;

public sealed record Board
{
    internal Board() { }

    public long Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public IReadOnlySet<long> TopicIds { get; init; } = new HashSet<long>();

    public DateTimeOffset UpdatedAt { get; init; }
}