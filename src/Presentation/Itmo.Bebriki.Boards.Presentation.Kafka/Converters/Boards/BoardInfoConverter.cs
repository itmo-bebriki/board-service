using Google.Protobuf.WellKnownTypes;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Events;
using Itmo.Bebriki.Boards.Kafka.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Kafka.Converters.Boards;

internal static class BoardInfoConverter
{
    internal static BoardInfoValue ToValue(CreateBoardEvent evt)
    {
        return new BoardInfoValue
        {
            BoardCreated = new BoardInfoValue.Types.BoardCreated
            {
                BoardId = evt.BoardId,
                Name = evt.Name,
                Description = evt.Description,
                TopicIds = { evt.TopicIds.ToArray() },
                CreatedAt = evt.CreatedAt.ToTimestamp(),
            },
        };
    }

    internal static BoardInfoValue ToValue(UpdateBoardEvent evt)
    {
        return new BoardInfoValue
        {
            BoardUpdated = new BoardInfoValue.Types.BoardUpdated
            {
                BoardId = evt.BoardId,
                Name = evt.Name,
                Description = evt.Description,
                UpdatedAt = evt.UpdatedAt.ToTimestamp(),
            },
        };
    }

    internal static BoardInfoValue ToValue(AddBoardTopicsEvent evt)
    {
        return new BoardInfoValue
        {
            BoardTopicsAdded = new BoardInfoValue.Types.BoardTopicsAdded
            {
                BoardId = evt.BoardId,
                AddedTopics = { evt.TopicIds.ToArray() },
            },
        };
    }

    internal static BoardInfoValue ToValue(RemoveBoardTopicsEvent evt)
    {
        return new BoardInfoValue
        {
            BoardTopicsRemoved = new BoardInfoValue.Types.BoardTopicsRemoved
            {
                BoardId = evt.BoardId,
                RemovedTopics = { evt.TopicIds.ToArray() },
            },
        };
    }
}