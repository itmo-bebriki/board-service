using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Boards;
using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Repositories.Boards;
using Itmo.Bebriki.Boards.Application.Models.Boards;

namespace Itmo.Bebriki.Boards.Infrastructure.Persistence.Repositories;

internal sealed class BoardRepository : IBoardRepository
{
    public IAsyncEnumerable<Board> QueryAsync(BoardQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<long> AddAsync(IReadOnlyCollection<Board> boards, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(IReadOnlyCollection<Board> boards, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddTopicsAsync(BoardTopicsQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveTopicsAsync(BoardTopicsQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}