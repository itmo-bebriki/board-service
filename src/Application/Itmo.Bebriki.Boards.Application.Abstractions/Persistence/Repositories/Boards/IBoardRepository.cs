using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Boards;
using Itmo.Bebriki.Boards.Application.Models.Boards;

namespace Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Repositories.Boards;

public interface IBoardRepository
{
    IAsyncEnumerable<Board> QueryAsync(
        BoardQuery query,
        CancellationToken cancellationToken);

    IAsyncEnumerable<long> AddAsync(
        BoardQuery query,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        IReadOnlyCollection<Board> boards,
        CancellationToken cancellationToken);

    Task AddTopicsAsync(
        BoardTopicsQuery query,
        CancellationToken cancellationToken);

    Task RemoveTopicsAsync(
        BoardTopicsQuery query,
        CancellationToken cancellationToken);
}