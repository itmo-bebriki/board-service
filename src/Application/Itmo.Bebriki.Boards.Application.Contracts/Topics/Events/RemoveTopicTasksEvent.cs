using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Boards.Application.Contracts.Topics.Events;

public sealed record RemoveTopicTasksEvent(long TopicId, IReadOnlySet<long> TaskIds) : IEvent;