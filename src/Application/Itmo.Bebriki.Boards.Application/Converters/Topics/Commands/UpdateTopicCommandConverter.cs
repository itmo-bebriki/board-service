using Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;
using Itmo.Bebriki.Boards.Application.Models.Topics;
using Itmo.Bebriki.Boards.Application.Models.Topics.Contexts;

namespace Itmo.Bebriki.Boards.Application.Converters.Topics.Commands;

internal static class UpdateTopicCommandConverter
{
    internal static UpdateTopicContext ToContext(
        UpdateTopicCommand command,
        Topic prevTopic,
        DateTimeOffset updatedAt)
    {
        return new UpdateTopicContext(
            TopicId: prevTopic.Id,
            Name: command.Name ?? prevTopic.Name,
            Description: command.Description ?? prevTopic.Description,
            UpdatedAt: updatedAt);
    }
}