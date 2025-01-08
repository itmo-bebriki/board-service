using Grpc.Core;
using Itmo.Bebriki.Boards.Contracts;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Controllers;

public sealed class BoardController : BoardService.BoardServiceBase
{
    public override Task<QueryBoardResponse> QueryBoard(QueryBoardRequest request, ServerCallContext context)
    {
        return base.QueryBoard(request, context);
    }

    public override Task<CreateBoardResponse> CreateBoard(CreateBoardRequest request, ServerCallContext context)
    {
        return base.CreateBoard(request, context);
    }

    public override Task<UpdateBoardResponse> UpdateBoard(UpdateBoardRequest request, ServerCallContext context)
    {
        return base.UpdateBoard(request, context);
    }

    public override Task<AddBoardTopicsResponse> AddBoardTopics(SetBoardTopicsRequest request, ServerCallContext context)
    {
        return base.AddBoardTopics(request, context);
    }

    public override Task<RemoveBoardTopicsResponse> RemoveBoardTopics(SetBoardTopicsRequest request, ServerCallContext context)
    {
        return base.RemoveBoardTopics(request, context);
    }
}