using SourceKit.Generators.Builder.Annotations;

namespace Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Topics;

[GenerateBuilder]
public sealed partial record TopicQuery(
    long[] TopicIds,
    DateTimeOffset? FromUpdatedAt,
    DateTimeOffset? ToUpdatedAt,
    long? Cursor,
    [RequiredValue] int PageSize);