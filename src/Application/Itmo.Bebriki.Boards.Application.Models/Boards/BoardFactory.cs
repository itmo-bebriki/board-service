using Itmo.Bebriki.Boards.Application.Models.Boards.Contexts;

namespace Itmo.Bebriki.Boards.Application.Models.Boards;

public static class BoardFactory
{
    public static Board CreateNew(
        long id,
        string name,
        string description,
        IReadOnlySet<long> topicIds,
        DateTimeOffset updatedAt)
    {
        return new Board
        {
            Id = id,
            Name = name,
            Description = description,
            TopicIds = topicIds,
            UpdatedAt = updatedAt,
        };
    }

    public static Board CreateFromCreateContext(CreateBoardContext context)
    {
        return new Board();
    }

    public static Board CreateFromUpdateContext(UpdateBoardContext context)
    {
        return new Board();
    }
}