using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Topics;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;

namespace Itmo.Bebriki.Boards.Application.Converters.Topics.Commands;

internal static class SetTopicTasksCommandConverter
{
    internal static TopicTasksQuery ToQuery(SetTopicTasksCommand command)
    {
        return TopicTasksQuery.Build(builder => builder
            .WithTopicId(command.TopicId)
            .WithTaskIds(command.TaskIds));
    }
}