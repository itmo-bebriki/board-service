using Itmo.Bebriki.Boards.Application.Models.Boards.Contexts;

namespace Itmo.Bebriki.Boards.Application.Models.Boards;

public static class BoardFactory
{
    public static Board CreateNew(
        long id,
        string name,
        string description,
        IReadOnlyCollection<long> topicIds,
        DateTimeOffset updatedAt)
    {
        return new Board
        {
            Id = id,
            Name = name,
            Description = description,
            TopicIds = new HashSet<long>(topicIds),
            UpdatedAt = updatedAt,
        };
    }

    public static Board CreateFromCreateContext(CreateBoardContext context)
    {
        return new Board
        {
            Name = context.Name,
            Description = context.Description,
            TopicIds = context.TopicIds,
            UpdatedAt = context.CreatedAt,
        };
    }

    public static Board CreateFromUpdateContext(UpdateBoardContext context)
    {
        return new Board
        {
            Id = context.BoardId,
            Name = context.Name,
            Description = context.Description,
            TopicIds = context.TopicIds,
            UpdatedAt = context.UpdatedAt,
        };
    }
}