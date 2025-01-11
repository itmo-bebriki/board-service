using Itmo.Bebriki.Boards.Application.Contracts.Topics.Dtos;
using Itmo.Bebriki.Boards.Application.Models.Topics;

namespace Itmo.Bebriki.Boards.Application.Converters.Topics.Dtos;

internal static class TopicDtoConverter
{
    internal static TopicDto ToDto(Topic topic)
    {
        return new TopicDto(
            Id: topic.Id,
            Name: topic.Name,
            Description: topic.Description,
            TaskIds: topic.TaskIds,
            UpdatedAt: topic.UpdatedAt);
    }
}