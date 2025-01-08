using Grpc.Core;
using Itmo.Bebriki.Topics.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Controllers;

public sealed class TopicController : TopicService.TopicServiceBase
{
    public override Task<QueryTopicResponse> QueryTopic(QueryTopicRequest request, ServerCallContext context)
    {
        return base.QueryTopic(request, context);
    }

    public override Task<CreateTopicResponse> CreateTopic(CreateTopicRequest request, ServerCallContext context)
    {
        return base.CreateTopic(request, context);
    }

    public override Task<UpdateTopicResponse> UpdateTopic(UpdateTopicRequest request, ServerCallContext context)
    {
        return base.UpdateTopic(request, context);
    }

    public override Task<AddTopicTasksResponse> AddTopicTasks(SetTopicTasksRequest request, ServerCallContext context)
    {
        return base.AddTopicTasks(request, context);
    }

    public override Task<RemoveTopicTasksResponse> RemoveTopicTasks(SetTopicTasksRequest request, ServerCallContext context)
    {
        return base.RemoveTopicTasks(request, context);
    }
}