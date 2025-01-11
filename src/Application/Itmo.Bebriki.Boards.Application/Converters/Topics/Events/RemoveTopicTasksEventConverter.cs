using Itmo.Bebriki.Boards.Application.Contracts.Topics.Events;

namespace Itmo.Bebriki.Boards.Application.Converters.Topics.Events;

internal static class RemoveTopicTasksEventConverter
{
    internal static RemoveTopicTasksEvent ToEvent(long topicId, IReadOnlySet<long> taskIds)
    {
        return new RemoveTopicTasksEvent(topicId, taskIds);
    }
}