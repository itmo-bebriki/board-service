using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Topics;
using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Repositories.Topics;
using Itmo.Bebriki.Boards.Application.Models.Topics;

namespace Itmo.Bebriki.Boards.Infrastructure.Persistence.Repositories;

internal sealed class TopicRepository : ITopicRepository
{
    public IAsyncEnumerable<Topic> QueryAsync(TopicQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<long> AddAsync(IReadOnlyCollection<Topic> query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(IReadOnlyCollection<Topic> topics, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddTasksAsync(TopicTasksQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveTasksAsync(TopicTasksQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}