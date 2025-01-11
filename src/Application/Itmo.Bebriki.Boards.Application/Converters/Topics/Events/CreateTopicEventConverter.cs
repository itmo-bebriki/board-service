using Itmo.Bebriki.Boards.Application.Contracts.Topics.Events;
using Itmo.Bebriki.Boards.Application.Models.Topics;

namespace Itmo.Bebriki.Boards.Application.Converters.Topics.Events;

internal static class CreateTopicEventConverter
{
    internal static CreateTopicEvent ToEvent(long topicId, Topic topic)
    {
        return new CreateTopicEvent(
            TopicId: topicId,
            Name: topic.Name,
            Description: topic.Description,
            TaskIds: topic.TaskIds,
            CreatedAt: topic.UpdatedAt);
    }
}