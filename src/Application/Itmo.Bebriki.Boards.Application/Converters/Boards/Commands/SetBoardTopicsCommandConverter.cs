using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Boards;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;

namespace Itmo.Bebriki.Boards.Application.Converters.Boards.Commands;

internal static class SetBoardTopicsCommandConverter
{
    internal static BoardTopicsQuery ToQuery(SetBoardTopicsCommand command)
    {
        return BoardTopicsQuery.Build(builder => builder
            .WithBoardId(command.BoardId)
            .WithTopicIds(command.TopicIds));
    }
}