using Google.Protobuf.WellKnownTypes;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Dtos;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Topics.Dtos;

internal static class TopicDtoConverter
{
    internal static Bebriki.Topics.Contracts.TopicDto FromInternal(TopicDto internalDto)
    {
        return new Bebriki.Topics.Contracts.TopicDto
        {
            TopicId = internalDto.Id,
            Name = internalDto.Name,
            Description = internalDto.Description,
            TaskIds = { internalDto.TaskIds.ToArray() },
            UpdatedAt = internalDto.UpdatedAt.ToTimestamp(),
        };
    }
}