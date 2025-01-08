using Itmo.Bebriki.Boards.Application.Contracts.Topics.Dtos;
using Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Topics.Dtos;
using Itmo.Bebriki.Topics.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Topics.Responses;

internal static class QueryTopicResponseConverter
{
    internal static QueryTopicResponse FromInternal(PagedTopicDto dto)
    {
        return new QueryTopicResponse
        {
            Cursor = dto.Cursor,
            Topics = { dto.TopicDtos.Select(TopicDtoConverter.FromInternal) },
        };
    }
}