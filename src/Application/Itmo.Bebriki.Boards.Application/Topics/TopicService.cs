using Itmo.Bebriki.Boards.Application.Abstractions.Persistence;
using Itmo.Bebriki.Boards.Application.Boards;
using Itmo.Bebriki.Boards.Application.Contracts.Topics;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Dtos;
using Itmo.Dev.Platform.Common.DateTime;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Persistence.Abstractions.Transactions;
using Microsoft.Extensions.Logging;

namespace Itmo.Bebriki.Boards.Application.Topics;

public sealed class TopicService : ITopicService
{
    private readonly IPersistenceContext _persistenceContext;
    private readonly IPersistenceTransactionProvider _transactionProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<BoardService> _logger;

    public TopicService(
        IPersistenceContext persistenceContext,
        IPersistenceTransactionProvider transactionProvider,
        IDateTimeProvider dateTimeProvider,
        IEventPublisher eventPublisher,
        ILogger<BoardService> logger)
    {
        _persistenceContext = persistenceContext;
        _transactionProvider = transactionProvider;
        _dateTimeProvider = dateTimeProvider;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public Task<PagedTopicDto> QueryTopicAsync(QueryTopicCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<long> CreateTopicAsync(CreateTopicCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTopicAsync(UpdateTopicCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddTopicTasksAsync(SetTopicTasksCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveTopicTasksAsync(SetTopicTasksCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}