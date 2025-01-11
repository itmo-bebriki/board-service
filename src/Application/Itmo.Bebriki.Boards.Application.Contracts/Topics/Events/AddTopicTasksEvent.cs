using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Boards.Application.Contracts.Topics.Events;

public sealed record AddTopicTasksEvent(long TopicId, IReadOnlySet<long> TaskIds) : IEvent;