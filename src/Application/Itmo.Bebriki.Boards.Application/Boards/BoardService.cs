using Itmo.Bebriki.Boards.Application.Abstractions.Persistence;
using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Boards;
using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Topics;
using Itmo.Bebriki.Boards.Application.Contracts.Boards;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Dtos;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Events;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Exceptions;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Exceptions;
using Itmo.Bebriki.Boards.Application.Converters.Boards.Commands;
using Itmo.Bebriki.Boards.Application.Converters.Boards.Dtos;
using Itmo.Bebriki.Boards.Application.Converters.Boards.Events;
using Itmo.Bebriki.Boards.Application.Models.Boards;
using Itmo.Bebriki.Boards.Application.Models.Boards.Contexts;
using Itmo.Dev.Platform.Common.DateTime;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Persistence.Abstractions.Transactions;
using Microsoft.Extensions.Logging;
using System.Data;

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

    public async Task<PagedBoardDto> QueryBoardAsync(QueryBoardCommand command, CancellationToken cancellationToken)
    {
        BoardQuery boardQuery = QueryBoardCommandConverter.ToQuery(command);

        HashSet<BoardDto> boardDtos = await _persistenceContext.Boards
            .QueryAsync(boardQuery, cancellationToken)
            .Select(BoardDtoConverter.ToDto)
            .ToHashSetAsync(cancellationToken);

        long? cursor = boardDtos.Count == command.PageSize && boardDtos.Count > 0
            ? boardDtos.Last().Id
            : null;

        return new PagedBoardDto(cursor, boardDtos);
    }

    public async Task<long> CreateBoardAsync(CreateBoardCommand command, CancellationToken cancellationToken)
    {
        await CheckForExistingTopicsAsync(command.TopicIds, cancellationToken);

        CreateBoardContext context = CreateBoardCommandConverter.ToContext(command, _dateTimeProvider.Current);
        Board board = BoardFactory.CreateFromCreateContext(context);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            long boardId = await _persistenceContext.Boards
                .AddAsync([board], cancellationToken)
                .FirstAsync(cancellationToken);

            CreateBoardEvent createBoardEvent = CreateBoardEventConverter.ToEvent(boardId, board);

            await _eventPublisher.PublishAsync(createBoardEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return boardId;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to create board. Name: {Name}, Description: {Description}, Topics: {Topics}",
                board.Name,
                board.Description,
                string.Join(", ", board.TopicIds));

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task UpdateBoardAsync(UpdateBoardCommand command, CancellationToken cancellationToken)
    {
        var boardQuery = BoardQuery.Build(builder => builder
            .WithBoardId(command.BoardId)
            .WithPageSize(1));

        Board? board = await _persistenceContext.Boards
            .QueryAsync(boardQuery, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (board is null)
        {
            _logger.LogWarning("Board with id: {Id} not found", command.BoardId);
            throw new BoardNotFoundException($"Board with id: {command.BoardId} not found");
        }

        UpdateBoardContext context = UpdateBoardCommandConverter.ToContext(command, board, _dateTimeProvider.Current);
        Board updatedBoard = BoardFactory.CreateFromUpdateContext(context);

        UpdateBoardEvent updateBoardEvent = UpdateBoardEventConverter.ToEvent(board, updatedBoard);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            await _persistenceContext.Boards.UpdateAsync([updatedBoard], cancellationToken);

            await _eventPublisher.PublishAsync(updateBoardEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to update board. BoardId: {BoardId}, Updated Fields: {UpdatedFields}",
                updatedBoard.Id,
                GetUpdatedFields(command));

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task AddBoardTopicsAsync(SetBoardTopicsCommand command, CancellationToken cancellationToken)
    {
        await CheckForExistingBoardAsync(command.BoardId, cancellationToken);
        await CheckForExistingTopicsAsync(command.TopicIds, cancellationToken);

        BoardTopicsQuery boardTopicsQuery = SetBoardTopicsCommandConverter.ToQuery(command);

        AddBoardTopicsEvent addBoardTopicsEvent =
            AddBoardTopicsEventConverter.ToEvent(command.BoardId, command.TopicIds);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            await _persistenceContext.Boards.AddTopicsAsync(boardTopicsQuery, cancellationToken);

            await _eventPublisher.PublishAsync(addBoardTopicsEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to add topics to board. BoardId: {BoardId}, Topics: {Topics}",
                command.BoardId,
                string.Join(", ", command.TopicIds));

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task RemoveBoardTopicsAsync(SetBoardTopicsCommand command, CancellationToken cancellationToken)
    {
        await CheckForExistingBoardAsync(command.BoardId, cancellationToken);
        await CheckForExistingTopicsAsync(command.TopicIds, cancellationToken);

        BoardTopicsQuery boardTopicsQuery = SetBoardTopicsCommandConverter.ToQuery(command);

        RemoveBoardTopicsEvent removeBoardTopicsEvent =
            RemoveBoardTopicsEventConverter.ToEvent(command.BoardId, command.TopicIds);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            await _persistenceContext.Boards.RemoveTopicsAsync(boardTopicsQuery, cancellationToken);

            await _eventPublisher.PublishAsync(removeBoardTopicsEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to remove topics from board. BoardId: {BoardId}, Topics: {Topics}",
                command.BoardId,
                string.Join(", ", command.TopicIds));

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static string GetUpdatedFields(UpdateBoardCommand command)
    {
        var updatedFields = new List<string>();

        if (command.Name != null) updatedFields.Add(nameof(command.Name));
        if (command.Description != null) updatedFields.Add(nameof(command.Description));

        return updatedFields.Count != 0 ? string.Join(", ", updatedFields) : "None";
    }

    private async Task CheckForExistingBoardAsync(long boardId, CancellationToken cancellationToken)
    {
        var boardQuery = BoardQuery.Build(builder => builder
            .WithBoardId(boardId)
            .WithPageSize(1));

        Board? board = await _persistenceContext.Boards
            .QueryAsync(boardQuery, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (board is null)
        {
            _logger.LogWarning("Board with id: {Id} not found", boardId);
            throw new BoardNotFoundException($"Board with id: {boardId} not found");
        }
    }

    private async Task CheckForExistingTopicsAsync(IReadOnlySet<long> topicIds, CancellationToken cancellationToken)
    {
        var topicQuery = TopicQuery.Build(builder => builder
            .WithTopicIds(topicIds)
            .WithPageSize(topicIds.Count));

        List<long> existingTopicIds = await _persistenceContext.Topics
            .QueryAsync(topicQuery, cancellationToken)
            .Select(t => t.Id)
            .ToListAsync(cancellationToken);

        var missingTopicIds = topicIds.Except(existingTopicIds).ToList();

        if (missingTopicIds.Count != 0)
        {
            string missingTopicsMessage = string.Join(", ", missingTopicIds);

            _logger.LogWarning("Topics with ids: {TopicIds} not found", missingTopicsMessage);
            throw new TopicNotFoundException($"Topics with ids {missingTopicsMessage} not found");
        }
    }
}