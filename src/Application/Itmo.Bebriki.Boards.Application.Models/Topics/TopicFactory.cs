using Itmo.Bebriki.Boards.Application.Models.Topics.Contexts;

namespace Itmo.Bebriki.Boards.Application.Models.Topics;

public static class TopicFactory
{
    public static Topic CreateNew(
        long id,
        string name,
        string description,
        IReadOnlySet<long> taskIds,
        DateTimeOffset updatedAt)
    {
        return new Topic
        {
            Id = id,
            Name = name,
            Description = description,
            TaskIds = taskIds,
            UpdatedAt = updatedAt,
        };
    }

    public static Topic CreateFromCreateContext(CreateTopicContext context)
    {
        return new Topic();
    }

    public static Topic CreateFromUpdateContext(UpdateTopicContext context)
    {
        return new Topic();
    }
}