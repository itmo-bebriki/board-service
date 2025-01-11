using Grpc.Core;
using Itmo.Bebriki.Boards.Application.Contracts.Boards;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Dtos;
using Itmo.Bebriki.Boards.Contracts;
using Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Boards.Requests;
using Itmo.Bebriki.Boards.Presentation.Grpc.Converters.Boards.Responses;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Controllers;

public sealed class BoardController : BoardService.BoardServiceBase
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    public override async Task<QueryBoardResponse> QueryBoard(QueryBoardRequest request, ServerCallContext context)
    {
        QueryBoardCommand internalCommand = QueryBoardRequestConverter.ToInternal(request);

        PagedBoardDto internalResponse =
            await _boardService.QueryBoardAsync(internalCommand, context.CancellationToken);

        QueryBoardResponse response = QueryBoardResponseConverter.FromInternal(internalResponse);

        return response;
    }

    public override async Task<CreateBoardResponse> CreateBoard(CreateBoardRequest request, ServerCallContext context)
    {
        CreateBoardCommand internalCommand = CreateBoardRequestConverter.ToInternal(request);

        long internalResponse = await _boardService.CreateBoardAsync(internalCommand, context.CancellationToken);

        CreateBoardResponse response = CreateBoardResponseConverter.FromInternal(internalResponse);

        return response;
    }

    public override async Task<UpdateBoardResponse> UpdateBoard(UpdateBoardRequest request, ServerCallContext context)
    {
        UpdateBoardCommand internalCommand = UpdateBoardRequestConverter.ToInternal(request);

        await _boardService.UpdateBoardAsync(internalCommand, context.CancellationToken);

        return new UpdateBoardResponse();
    }

    public override async Task<AddBoardTopicsResponse> AddBoardTopics(
        SetBoardTopicsRequest request,
        ServerCallContext context)
    {
        SetBoardTopicsCommand internalCommand = SetBoardTopicsRequestConverter.ToInternal(request);

        await _boardService.AddBoardTopicsAsync(internalCommand, context.CancellationToken);

        return new AddBoardTopicsResponse();
    }

    public override async Task<RemoveBoardTopicsResponse> RemoveBoardTopics(
        SetBoardTopicsRequest request,
        ServerCallContext context)
    {
        SetBoardTopicsCommand internalCommand = SetBoardTopicsRequestConverter.ToInternal(request);

        await _boardService.RemoveBoardTopicsAsync(internalCommand, context.CancellationToken);

        return new RemoveBoardTopicsResponse();
    }
}