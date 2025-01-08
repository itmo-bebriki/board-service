using SourceKit.Generators.Builder.Annotations;

namespace Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Topics;

[GenerateBuilder]
public sealed partial record TopicTasksQuery(
    [RequiredValue] long TopicId,
    [RequiredValue] long[] TaskIds);