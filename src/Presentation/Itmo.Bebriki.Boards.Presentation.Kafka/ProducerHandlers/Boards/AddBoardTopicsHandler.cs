using Itmo.Bebriki.Boards.Application.Contracts.Boards.Events;
using Itmo.Bebriki.Boards.Kafka.Contracts;
using Itmo.Bebriki.Boards.Presentation.Kafka.Converters.Boards;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.Kafka.Producer;

namespace Itmo.Bebriki.Boards.Presentation.Kafka.ProducerHandlers.Boards;

internal sealed class AddBoardTopicsHandler : IEventHandler<AddBoardTopicsEvent>
{
    private readonly IKafkaMessageProducer<BoardInfoKey, BoardInfoValue> _producer;

    public AddBoardTopicsHandler(IKafkaMessageProducer<BoardInfoKey, BoardInfoValue> producer)
    {
        _producer = producer;
    }

    public async ValueTask HandleAsync(AddBoardTopicsEvent evt, CancellationToken cancellationToken)
    {
        var key = new BoardInfoKey { BoardId = evt.BoardId };
        BoardInfoValue value = BoardInfoConverter.ToValue(evt);

        var message = new KafkaProducerMessage<BoardInfoKey, BoardInfoValue>(key, value);
        await _producer.ProduceAsync(message, cancellationToken);
    }
}