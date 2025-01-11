using Itmo.Bebriki.Boards.Application.Contracts.Boards.Events;

namespace Itmo.Bebriki.Boards.Application.Converters.Boards.Events;

internal static class RemoveBoardTopicsEventConverter
{
    internal static RemoveBoardTopicsEvent ToEvent(long boardId, IReadOnlySet<long> topicIds)
    {
        return new RemoveBoardTopicsEvent(boardId, topicIds);
    }
}