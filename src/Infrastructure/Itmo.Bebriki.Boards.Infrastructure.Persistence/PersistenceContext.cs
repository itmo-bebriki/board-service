using Itmo.Bebriki.Boards.Application.Abstractions.Persistence;
using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Repositories.Boards;
using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Repositories.Topics;

namespace Itmo.Bebriki.Boards.Infrastructure.Persistence;

public class PersistenceContext : IPersistenceContext
{
    public PersistenceContext(IBoardRepository boards, ITopicRepository topics)
    {
        Boards = boards;
        Topics = topics;
    }

    public IBoardRepository Boards { get; }

    public ITopicRepository Topics { get; }
}