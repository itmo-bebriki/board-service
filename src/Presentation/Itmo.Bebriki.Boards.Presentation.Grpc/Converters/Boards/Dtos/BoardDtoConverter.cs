using Google.Protobuf.WellKnownTypes;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Dtos;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Boards.Dtos;

internal static class BoardDtoConverter
{
    internal static Contracts.BoardDto FromInternal(BoardDto internalDto)
    {
        return new Contracts.BoardDto
        {
            BoardId = internalDto.Id,
            Name = internalDto.Name,
            Description = internalDto.Description,
            TopicIds = { internalDto.TopicIds.ToArray() },
            UpdatedAt = internalDto.UpdatedAt.ToTimestamp(),
        };
    }
}