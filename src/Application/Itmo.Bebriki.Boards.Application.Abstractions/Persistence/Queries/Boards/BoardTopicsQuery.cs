using SourceKit.Generators.Builder.Annotations;

namespace Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Queries.Boards;

[GenerateBuilder]
public sealed partial record BoardTopicsQuery(
    [RequiredValue] long BoardId,
    [RequiredValue] long[] TopicIds);