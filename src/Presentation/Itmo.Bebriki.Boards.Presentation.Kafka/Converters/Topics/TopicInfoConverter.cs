using Google.Protobuf.WellKnownTypes;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Events;
using Itmo.Bebriki.Topics.Kafka.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Kafka.Converters.Topics;

internal static class TopicInfoConverter
{
    internal static TopicInfoValue ToValue(CreateTopicEvent evt)
    {
        return new TopicInfoValue
        {
            TopicCreated = new TopicInfoValue.Types.TopicCreated
            {
                TopicId = evt.TopicId,
                Name = evt.Name,
                Description = evt.Description,
                TaskIds = { evt.TaskIds.ToArray() },
                CreatedAt = evt.CreatedAt.ToTimestamp(),
            },
        };
    }

    internal static TopicInfoValue ToValue(UpdateTopicEvent evt)
    {
        return new TopicInfoValue
        {
            TopicUpdated = new TopicInfoValue.Types.TopicUpdated
            {
                TopicId = evt.TopicId,
                Name = evt.Name,
                Description = evt.Description,
                UpdatedAt = evt.UpdatedAt.ToTimestamp(),
            },
        };
    }

    internal static TopicInfoValue ToValue(AddTopicTasksEvent evt)
    {
        return new TopicInfoValue
        {
            TopicTasksAdded = new TopicInfoValue.Types.TopicTasksAdded
            {
                TopicId = evt.TopicId,
                AddedTasks = { evt.TaskIds.ToArray() },
            },
        };
    }

    internal static TopicInfoValue ToValue(RemoveTopicTasksEvent evt)
    {
        return new TopicInfoValue
        {
            TopicTasksRemoved = new TopicInfoValue.Types.TopicTasksRemoved
            {
                TopicId = evt.TopicId,
                RemovedTasks = { evt.TaskIds.ToArray() },
            },
        };
    }
}