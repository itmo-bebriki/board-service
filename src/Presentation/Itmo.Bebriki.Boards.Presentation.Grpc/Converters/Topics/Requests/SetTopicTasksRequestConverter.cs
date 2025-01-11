using Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;
using Itmo.Bebriki.Topics.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Topics.Requests;

internal static class SetTopicTasksRequestConverter
{
    internal static SetTopicTasksCommand ToInternal(SetTopicTasksRequest request)
    {
        return new SetTopicTasksCommand(
            TopicId: request.TopicId,
            TaskIds: request.TaskIds.ToHashSet());
    }
}