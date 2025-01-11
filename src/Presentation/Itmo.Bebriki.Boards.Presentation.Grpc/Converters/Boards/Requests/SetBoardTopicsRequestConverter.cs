using Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;
using Itmo.Bebriki.Boards.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Boards.Requests;

internal static class SetBoardTopicsRequestConverter
{
    internal static SetBoardTopicsCommand ToInternal(SetBoardTopicsRequest request)
    {
        return new SetBoardTopicsCommand(
            BoardId: request.BoardId,
            TopicIds: request.TopicIds.ToHashSet());
    }
}