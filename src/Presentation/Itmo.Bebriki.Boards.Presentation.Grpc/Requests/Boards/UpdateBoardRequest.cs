using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Boards.Contracts;

public sealed partial class UpdateBoardRequest : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BoardId <= 0)
        {
            yield return new ValidationResult(
                "Board ID is required and must be greater than zero.",
                [nameof(BoardId)]);
        }
    }
}