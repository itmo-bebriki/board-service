using Itmo.Bebriki.Boards.Application.Contracts.Topics.Events;

namespace Itmo.Bebriki.Boards.Application.Converters.Topics.Events;

internal static class AddTopicTasksEventConverter
{
    internal static AddTopicTasksEvent ToEvent(long topicId, IReadOnlySet<long> taskIds)
    {
        return new AddTopicTasksEvent(topicId, taskIds);
    }
}