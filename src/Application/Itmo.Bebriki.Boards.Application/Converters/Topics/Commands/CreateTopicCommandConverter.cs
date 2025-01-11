using Itmo.Bebriki.Boards.Application.Contracts.Topics.Commands;
using Itmo.Bebriki.Boards.Application.Models.Topics.Contexts;

namespace Itmo.Bebriki.Boards.Application.Converters.Topics.Commands;

internal static class CreateTopicCommandConverter
{
    internal static CreateTopicContext ToContext(CreateTopicCommand command, DateTimeOffset createdAt)
    {
        return new CreateTopicContext(
            Name: command.Name,
            Description: command.Description,
            TaskIds: command.TaskIds,
            CreatedAt: createdAt);
    }
}