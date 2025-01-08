using Itmo.Bebriki.Boards.Application.Abstractions.Persistence;
using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Topics;
using Itmo.Bebriki.Boards.Application.Boards;
using Itmo.Bebriki.Boards.Application.Contracts.Topics;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Dtos;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Events;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Exceptions;
using Itmo.Bebriki.Boards.Application.Converters.Topics.Commands;
using Itmo.Bebriki.Boards.Application.Converters.Topics.Dtos;
using Itmo.Bebriki.Boards.Application.Converters.Topics.Events;
using Itmo.Bebriki.Boards.Application.Models.Topics;
using Itmo.Bebriki.Boards.Application.Models.Topics.Contexts;
using Itmo.Dev.Platform.Common.DateTime;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Persistence.Abstractions.Transactions;
using Microsoft.Extensions.Logging;
using System.Data;

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

    public async Task<PagedTopicDto> QueryTopicAsync(QueryTopicCommand command, CancellationToken cancellationToken)
    {
        TopicQuery topicQuery = QueryTopicCommandConverter.ToQuery(command);

        HashSet<TopicDto> topicDtos = await _persistenceContext.Topics
            .QueryAsync(topicQuery, cancellationToken)
            .Select(TopicDtoConverter.ToDto)
            .ToHashSetAsync(cancellationToken);

        long? cursor = topicDtos.Count == command.PageSize && topicDtos.Count > 0
            ? topicDtos.Last().Id
            : null;

        return new PagedTopicDto(cursor, topicDtos);
    }

    public async Task<long> CreateTopicAsync(CreateTopicCommand command, CancellationToken cancellationToken)
    {
        CreateTopicContext context = CreateTopicCommandConverter.ToContext(command, _dateTimeProvider.Current);
        Topic topic = TopicFactory.CreateFromCreateContext(context);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            long topicId = await _persistenceContext.Topics
                .AddAsync([topic], cancellationToken)
                .FirstAsync(cancellationToken);

            CreateTopicEvent createTopicEvent = CreateTopicEventConverter.ToEvent(topicId, topic);

            await _eventPublisher.PublishAsync(createTopicEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return topicId;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to create topic. Name: {Name}, Description: {Description}, Tasks: {Tasks}",
                topic.Name,
                topic.Description,
                string.Join(", ", topic.TaskIds));

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task UpdateTopicAsync(UpdateTopicCommand command, CancellationToken cancellationToken)
    {
        var topicQuery = TopicQuery.Build(builder => builder
            .WithTopicId(command.TopicId)
            .WithPageSize(1));

        Topic? topic = await _persistenceContext.Topics
            .QueryAsync(topicQuery, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (topic is null)
        {
            _logger.LogWarning("Topic with id: {Id} not found", command.TopicId);
            throw new TopicNotFoundException($"Board with id: {command.TopicId} not found");
        }

        UpdateTopicContext context = UpdateTopicCommandConverter.ToContext(command, topic, _dateTimeProvider.Current);
        Topic updatedTopic = TopicFactory.CreateFromUpdateContext(context);

        UpdateTopicEvent updateTopicEvent = UpdateTopicEventConverter.ToEvent(topic, updatedTopic);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            await _persistenceContext.Topics.UpdateAsync([updatedTopic], cancellationToken);

            await _eventPublisher.PublishAsync(updateTopicEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to update topic. TopicId: {TopicId}, Updated Fields: {UpdatedFields}",
                updatedTopic.Id,
                GetUpdatedFields(command));

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task AddTopicTasksAsync(SetTopicTasksCommand command, CancellationToken cancellationToken)
    {
        await CheckForExistingTopicAsync(command.TopicId, cancellationToken);

        TopicTasksQuery topicTasksQuery = SetTopicTasksCommandConverter.ToQuery(command);

        AddTopicTasksEvent addTopicTasksEvent =
            AddTopicTasksEventConverter.ToEvent(command.TopicId, command.TaskIds);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            await _persistenceContext.Topics.AddTasksAsync(topicTasksQuery, cancellationToken);

            await _eventPublisher.PublishAsync(addTopicTasksEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to add tasks to topic. TopicId: {TopicId}, Tasks: {Tasks}",
                command.TopicId,
                string.Join(", ", command.TaskIds));

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task RemoveTopicTasksAsync(SetTopicTasksCommand command, CancellationToken cancellationToken)
    {
        await CheckForExistingTopicAsync(command.TopicId, cancellationToken);

        TopicTasksQuery topicTasksQuery = SetTopicTasksCommandConverter.ToQuery(command);

        RemoveTopicTasksEvent removeTopicTasksEvent =
            RemoveTopicTasksEventConverter.ToEvent(command.TopicId, command.TaskIds);

        await using IPersistenceTransaction transaction = await _transactionProvider.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken);

        try
        {
            await _persistenceContext.Topics.RemoveTasksAsync(topicTasksQuery, cancellationToken);

            await _eventPublisher.PublishAsync(removeTopicTasksEvent, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to remove tasks from topic. TopicId: {TopicId}, Tasks: {Tasks}",
                command.TopicId,
                string.Join(", ", command.TaskIds));

            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static string GetUpdatedFields(UpdateTopicCommand command)
    {
        var updatedFields = new List<string>();

        if (command.Name != null) updatedFields.Add(nameof(command.Name));
        if (command.Description != null) updatedFields.Add(nameof(command.Description));

        return updatedFields.Count != 0 ? string.Join(", ", updatedFields) : "None";
    }

    private async Task CheckForExistingTopicAsync(long topicId, CancellationToken cancellationToken)
    {
        var topicQuery = TopicQuery.Build(builder => builder
            .WithTopicId(topicId)
            .WithPageSize(1));

        Topic? topic = await _persistenceContext.Topics
            .QueryAsync(topicQuery, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (topic is null)
        {
            _logger.LogWarning("Topic with id: {TopicId} not found", topicId);
            throw new TopicNotFoundException($"Topic with id: {topicId} not found");
        }
    }
}