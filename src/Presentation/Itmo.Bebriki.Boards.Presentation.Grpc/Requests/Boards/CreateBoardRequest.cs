using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Boards.Contracts;

public sealed partial class CreateBoardRequest : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Name))
        {
            yield return new ValidationResult(
                "Name field is required.",
                [nameof(Name)]);
        }

        if (string.IsNullOrEmpty(Description))
        {
            yield return new ValidationResult(
                "Description field is required.",
                [nameof(Description)]);
        }

        if (TopicIds.Any(id => id <= 0))
        {
            yield return new ValidationResult(
                "TopicIds contains invalid values. Topic ID is required and must be greater than zero.",
                [nameof(TopicIds)]);
        }
    }
}