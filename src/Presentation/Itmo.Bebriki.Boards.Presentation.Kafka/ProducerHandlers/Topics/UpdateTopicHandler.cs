using Itmo.Bebriki.Boards.Application.Contracts.Topics.Events;
using Itmo.Bebriki.Boards.Presentation.Kafka.Converters.Topics;
using Itmo.Bebriki.Topics.Kafka.Contracts;
using Itmo.Dev.Platform.Events;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.Kafka.Producer;

namespace Itmo.Bebriki.Boards.Presentation.Kafka.ProducerHandlers.Topics;

internal sealed class UpdateTopicHandler : IEventHandler<UpdateTopicEvent>
{
    private readonly IKafkaMessageProducer<TopicInfoKey, TopicInfoValue> _producer;

    public UpdateTopicHandler(IKafkaMessageProducer<TopicInfoKey, TopicInfoValue> producer)
    {
        _producer = producer;
    }

    public async ValueTask HandleAsync(UpdateTopicEvent evt, CancellationToken cancellationToken)
    {
        var key = new TopicInfoKey { TopicId = evt.TopicId };
        TopicInfoValue value = TopicInfoConverter.ToValue(evt);

        var message = new KafkaProducerMessage<TopicInfoKey, TopicInfoValue>(key, value);
        await _producer.ProduceAsync(message, cancellationToken);
    }
}