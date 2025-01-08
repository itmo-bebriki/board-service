using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Boards;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;

namespace Itmo.Bebriki.Boards.Application.Converters.Boards.Commands;

internal static class QueryBoardCommandConverter
{
    internal static BoardQuery ToQuery(QueryBoardCommand command)
    {
        return BoardQuery.Build(builder => builder
            .WithBoardIds(command.BoardIds)
            .WithFromUpdatedAt(command.FromUpdatedAt)
            .WithToUpdatedAt(command.ToUpdatedAt)
            .WithCursor(command.Cursor)
            .WithPageSize(command.PageSize));
    }
}