using Itmo.Bebriki.Boards.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Boards.Responses;

internal static class CreateBoardResponseConverter
{
    internal static CreateBoardResponse FromInternal(long boardId)
    {
        return new CreateBoardResponse
        {
            BoardId = boardId,
        };
    }
}