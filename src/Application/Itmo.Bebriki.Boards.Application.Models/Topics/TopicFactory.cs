using Itmo.Bebriki.Boards.Application.Models.Topics.Contexts;

namespace Itmo.Bebriki.Boards.Application.Models.Topics;

public static class TopicFactory
{
    public static Topic CreateNew(
        long id,
        string name,
        string description,
        IReadOnlyCollection<long> taskIds,
        DateTimeOffset updatedAt)
    {
        return new Topic
        {
            Id = id,
            Name = name,
            Description = description,
            TaskIds = new HashSet<long>(taskIds),
            UpdatedAt = updatedAt,
        };
    }

    public static Topic CreateFromCreateContext(CreateTopicContext context)
    {
        return new Topic
        {
            Name = context.Name,
            Description = context.Description,
            TaskIds = context.TaskIds,
            UpdatedAt = context.CreatedAt,
        };
    }

    public static Topic CreateFromUpdateContext(UpdateTopicContext context)
    {
        return new Topic
        {
            Id = context.TopicId,
            Name = context.Name,
            Description = context.Description,
            UpdatedAt = context.UpdatedAt,
        };
    }
}