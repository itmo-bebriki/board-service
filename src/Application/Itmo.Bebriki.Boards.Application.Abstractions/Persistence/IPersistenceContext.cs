using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Repositories.Boards;
using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Repositories.Topics;

namespace Itmo.Bebriki.Boards.Application.Abstractions.Persistence;

public interface IPersistenceContext
{
    IBoardRepository Boards { get; }

    ITopicRepository Topics { get; }
}