using Itmo.Bebriki.Boards.Application.Contracts.Boards.Commands;
using Itmo.Bebriki.Boards.Application.Models.Boards.Contexts;

namespace Itmo.Bebriki.Boards.Application.Converters.Boards.Commands;

internal static class CreateBoardCommandConverter
{
    internal static CreateBoardContext ToContext(CreateBoardCommand command, DateTimeOffset createdAt)
    {
        return new CreateBoardContext(
            Name: command.Name,
            Description: command.Description,
            TopicIds: command.TopicIds,
            CreatedAt: createdAt);
    }
}