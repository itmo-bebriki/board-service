using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Topics.Contracts;

public sealed partial class SetTopicTasksRequest : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (TopicId <= 0)
        {
            yield return new ValidationResult(
                "Topic ID is required and must be greater than zero.",
                [nameof(TopicId)]);
        }

        if (TaskIds.Any(id => id <= 0))
        {
            yield return new ValidationResult(
                "TaskIds contains invalid values. Task ID is required and must be greater than zero.",
                [nameof(TaskIds)]);
        }
    }
}