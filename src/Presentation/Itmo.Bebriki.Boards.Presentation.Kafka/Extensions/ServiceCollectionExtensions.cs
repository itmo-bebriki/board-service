using Itmo.Bebriki.Boards.Kafka.Contracts;
using Itmo.Bebriki.Topics.Kafka.Contracts;
using Itmo.Dev.Platform.Kafka.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Bebriki.Boards.Presentation.Kafka.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationKafka(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        const string configurationSection = "Presentation:Kafka";
        const string producerKey = "Presentation:Kafka:Producers";

        collection.AddPlatformKafka(kafka => kafka
            .ConfigureOptions(configuration.GetSection(configurationSection))
            .AddProducer(producer => producer
                .WithKey<BoardInfoKey>()
                .WithValue<BoardInfoValue>()
                .WithConfiguration(configuration.GetSection($"{producerKey}:BoardInfo"))
                .SerializeKeyWithProto()
                .SerializeValueWithProto())
            .AddProducer(producer => producer
                .WithKey<TopicInfoKey>()
                .WithValue<TopicInfoValue>()
                .WithConfiguration(configuration.GetSection($"{producerKey}:TopicInfo"))
                .SerializeKeyWithProto()
                .SerializeValueWithProto()));

        return collection;
    }
}