namespace Itmo.Bebriki.Boards.Application.Models.Topics;

public sealed record Topic
{
    internal Topic() { }

    public long Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public IReadOnlySet<long> TaskIds { get; init; } = new HashSet<long>();

    public DateTimeOffset UpdatedAt { get; init; }
}