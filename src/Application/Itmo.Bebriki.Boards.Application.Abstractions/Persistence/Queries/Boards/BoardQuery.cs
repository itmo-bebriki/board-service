using SourceKit.Generators.Builder.Annotations;

namespace Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Boards;

[GenerateBuilder]
public sealed partial record BoardQuery(
    long[] BoardIds,
    DateTimeOffset? FromUpdatedAt,
    DateTimeOffset? ToUpdatedAt,
    long? Cursor,
    [RequiredValue] int PageSize);