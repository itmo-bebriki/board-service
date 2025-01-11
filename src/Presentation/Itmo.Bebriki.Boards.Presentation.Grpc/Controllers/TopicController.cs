using Grpc.Core;
using Itmo.Bebriki.Boards.Application.Contracts.Topics;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Dtos;
using Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Topics.Requests;
using Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Topics.Responses;
using Itmo.Bebriki.Topics.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Controllers;

public sealed class TopicController : TopicService.TopicServiceBase
{
    private readonly ITopicService _topicService;

    public TopicController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    public override async Task<QueryTopicResponse> QueryTopic(QueryTopicRequest request, ServerCallContext context)
    {
        QueryTopicCommand internalCommand = QueryTopicRequestConverter.ToInternal(request);

        PagedTopicDto internalResponse =
            await _topicService.QueryTopicAsync(internalCommand, context.CancellationToken);

        QueryTopicResponse response = QueryTopicResponseConverter.FromInternal(internalResponse);

        return response;
    }

    public override async Task<CreateTopicResponse> CreateTopic(CreateTopicRequest request, ServerCallContext context)
    {
        CreateTopicCommand internalCommand = CreateTopicRequestConverter.ToInternal(request);

        long internalResponse = await _topicService.CreateTopicAsync(internalCommand, context.CancellationToken);

        CreateTopicResponse response = CreateTopicResponseConverter.FromInternal(internalResponse);

        return response;
    }

    public override async Task<UpdateTopicResponse> UpdateTopic(UpdateTopicRequest request, ServerCallContext context)
    {
        UpdateTopicCommand internalCommand = UpdateTopicRequestConverter.ToInternal(request);

        await _topicService.UpdateTopicAsync(internalCommand, context.CancellationToken);

        return new UpdateTopicResponse();
    }

    public override async Task<AddTopicTasksResponse> AddTopicTasks(
        SetTopicTasksRequest request,
        ServerCallContext context)
    {
        SetTopicTasksCommand internalCommand = SetTopicTasksRequestConverter.ToInternal(request);

        await _topicService.AddTopicTasksAsync(internalCommand, context.CancellationToken);

        return new AddTopicTasksResponse();
    }

    public override async Task<RemoveTopicTasksResponse> RemoveTopicTasks(
        SetTopicTasksRequest request,
        ServerCallContext context)
    {
        SetTopicTasksCommand internalCommand = SetTopicTasksRequestConverter.ToInternal(request);

        await _topicService.RemoveTopicTasksAsync(internalCommand, context.CancellationToken);

        return new RemoveTopicTasksResponse();
    }
}