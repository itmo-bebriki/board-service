using Itmo.Bebriki.Boards.Application.Contracts.Boards.Events;
using Itmo.Bebriki.Boards.Kafka.Contracts;
using Itmo.Bebriki.Boards.Presentation.Kafka.Converters.Boards;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.Kafka.Producer;

namespace Itmo.Bebriki.Boards.Presentation.Kafka.ProducerHandlers.Boards;

public sealed class RemoveBoardTopicsHandler : IEventHandler<RemoveBoardTopicsEvent>
{
    private readonly IKafkaMessageProducer<BoardInfoKey, BoardInfoValue> _producer;

    public RemoveBoardTopicsHandler(IKafkaMessageProducer<BoardInfoKey, BoardInfoValue> producer)
    {
        _producer = producer;
    }

    public async ValueTask HandleAsync(RemoveBoardTopicsEvent evt, CancellationToken cancellationToken)
    {
        var key = new BoardInfoKey { BoardId = evt.BoardId };
        BoardInfoValue value = BoardInfoConverter.ToValue(evt);

        var message = new KafkaProducerMessage<BoardInfoKey, BoardInfoValue>(key, value);
        await _producer.ProduceAsync(message, cancellationToken);
    }
}