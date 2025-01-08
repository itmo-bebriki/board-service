using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Topics.Contracts;

public sealed partial class CreateTopicRequest : IValidatableObject
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

        if (TaskIds.Any(id => id <= 0))
        {
            yield return new ValidationResult(
                "TaskIds contains invalid values. Task ID is required and must be greater than zero.",
                [nameof(TaskIds)]);
        }
    }
}