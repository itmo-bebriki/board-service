using Itmo.Bebriki.Boards.Application.Contracts.Topics.Events;
using Itmo.Bebriki.Boards.Application.Models.Topics;

namespace Itmo.Bebriki.Boards.Application.Converters.Topics.Events;

internal static class UpdateTopicEventConverter
{
    internal static UpdateTopicEvent ToEvent(Topic prevTopic, Topic updatedTopic)
    {
        return new UpdateTopicEvent(
            TopicId: updatedTopic.Id,
            UpdatedAt: updatedTopic.UpdatedAt,
            Name: prevTopic.Name == updatedTopic.Name ? null : updatedTopic.Name,
            Description: prevTopic.Description == updatedTopic.Description ? null : updatedTopic.Description);
    }
}