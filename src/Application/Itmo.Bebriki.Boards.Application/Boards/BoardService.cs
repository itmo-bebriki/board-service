using Itmo.Bebriki.Boards.Application.Abstractions.Persistence;
using Itmo.Bebriki.Boards.Application.Contracts.Boards;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Dtos;
using Itmo.Dev.Platform.Common.DateTime;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Persistence.Abstractions.Transactions;
using Microsoft.Extensions.Logging;

namespace Itmo.Bebriki.Boards.Application.Boards;

public sealed class BoardService : IBoardService
{
    private readonly IPersistenceContext _persistenceContext;
    private readonly IPersistenceTransactionProvider _transactionProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<BoardService> _logger;

    public BoardService(
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

    public Task<PagedBoardDto> QueryBoardAsync(QueryBoardCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<long> CreateBoardAsync(CreateBoardCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBoardAsync(UpdateBoardCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddBoardTopicsAsync(SetBoardTopicsCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveBoardTopicsAsync(SetBoardTopicsCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}