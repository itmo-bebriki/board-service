using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Topics.Contracts;

public sealed partial class QueryTopicRequest : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (TopicIds.Any(id => id <= 0))
        {
            yield return new ValidationResult(
                "TopicIds contains invalid values. Topic ID is required and must be greater than zero.",
                [nameof(TopicIds)]);
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