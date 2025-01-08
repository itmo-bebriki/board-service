using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Boards.Contracts;

public sealed partial class QueryBoardRequest : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BoardIds.Any(id => id <= 0))
        {
            yield return new ValidationResult(
                "BoardIds contains invalid values. Board ID is required and must be greater than zero.",
                [nameof(BoardIds)]);
        }

        if (Cursor < 0)
        {
            yield return new ValidationResult(
                "Cursor must be greater than or equal to 0.",
                [nameof(Cursor)]);
        }

        if (PageSize < 0)
        {
            yield return new ValidationResult(
                "PageSize must be greater than or equal to 0.",
                [nameof(PageSize)]);
        }
    }
}