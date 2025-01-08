using Itmo.Dev.Platform.Events;

namespace Itmo.Bebriki.Boards.Application.Contracts.Boards.Events;

public sealed record AddBoardTopicsEvent(long BoardId, IReadOnlySet<long> TopicIds) : IEvent;