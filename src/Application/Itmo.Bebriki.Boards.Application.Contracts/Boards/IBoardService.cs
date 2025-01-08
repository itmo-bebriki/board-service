using Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Dtos;

namespace Itmo.Bebriki.Boards.Application.Contracts.Boards;

public interface IBoardService
{
    Task<PagedBoardDto> QueryBoardAsync(
        QueryBoardCommand command,
        CancellationToken cancellationToken);

    Task<long> CreateBoardAsync(
        CreateBoardCommand command,
        CancellationToken cancellationToken);

    Task UpdateBoardAsync(
        UpdateBoardCommand command,
        CancellationToken cancellationToken);

    Task AddBoardTopicsAsync(
        SetBoardTopicsCommand command,
        CancellationToken cancellationToken);

    Task RemoveBoardTopicsAsync(
        SetBoardTopicsCommand command,
        CancellationToken cancellationToken);
}