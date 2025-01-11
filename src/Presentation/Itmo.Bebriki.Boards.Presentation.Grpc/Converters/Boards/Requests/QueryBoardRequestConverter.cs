using Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;
using Itmo.Bebriki.Boards.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Boards.Requests;

internal static class QueryBoardRequestConverter
{
    internal static QueryBoardCommand ToInternal(QueryBoardRequest request)
    {
        return new QueryBoardCommand(
            BoardIds: request.BoardIds?.ToArray() ?? [],
            FromUpdatedAt: request.FromUpdatedAt?.ToDateTimeOffset(),
            ToUpdatedAt: request.ToUpdatedAt?.ToDateTimeOffset(),
            Cursor: request.Cursor,
            PageSize: request.PageSize);
    }
}