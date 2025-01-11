using Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Dtos;

namespace Itmo.Bebriki.Boards.Application.Contracts.Topics;

public interface ITopicService
{
    Task<PagedTopicDto> QueryTopicAsync(
        QueryTopicCommand command,
        CancellationToken cancellationToken);

    Task<long> CreateTopicAsync(
        CreateTopicCommand command,
        CancellationToken cancellationToken);

    Task UpdateTopicAsync(
        UpdateTopicCommand command,
        CancellationToken cancellationToken);

    Task AddTopicTasksAsync(
        SetTopicTasksCommand command,
        CancellationToken cancellationToken);

    Task RemoveTopicTasksAsync(
        SetTopicTasksCommand command,
        CancellationToken cancellationToken);
}