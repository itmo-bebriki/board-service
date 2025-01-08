using Itmo.Bebriki.Boards.Application.Contracts.Boards.Events;

namespace Itmo.Bebriki.Boards.Application.Converters.Boards.Events;

internal static class AddBoardTopicsEventConverter
{
    internal static AddBoardTopicsEvent ToEvent(long boardId, IReadOnlySet<long> topicIds)
    {
        return new AddBoardTopicsEvent(boardId, topicIds);
    }
}