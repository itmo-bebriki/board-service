using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Topics;
using Itmo.Bebriki.Boards.Application.Models.Topics;

namespace Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Repositories.Topics;

public interface ITopicRepository
{
    IAsyncEnumerable<Topic> QueryAsync(
        TopicQuery query,
        CancellationToken cancellationToken);

    IAsyncEnumerable<long> AddAsync(
        TopicQuery query,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        IReadOnlyCollection<Topic> boards,
        CancellationToken cancellationToken);

    Task AddTasksAsync(
        TopicTasksQuery query,
        CancellationToken cancellationToken);

    Task RemoveTasksAsync(
        TopicTasksQuery query,
        CancellationToken cancellationToken);
}