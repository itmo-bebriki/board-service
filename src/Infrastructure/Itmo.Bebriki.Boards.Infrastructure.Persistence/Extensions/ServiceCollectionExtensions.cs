using Itmo.Bebriki.Boards.Application.Abstractions.Persistence;
using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Repositories.Boards;
using Itmo.Bebriki.Boards.Application.Abstractions.Persistence.Repositories.Topics;
using Itmo.Bebriki.Boards.Infrastructure.Persistence.Plugins;
using Itmo.Bebriki.Boards.Infrastructure.Persistence.Repositories;
using Itmo.Dev.Platform.Persistence.Abstractions.Extensions;
using Itmo.Dev.Platform.Persistence.Postgres.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Bebriki.Boards.Infrastructure.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection collection)
    {
        collection.AddPlatformPersistence(persistence => persistence
            .UsePostgres(postgres => postgres
                .WithConnectionOptions(b => b.BindConfiguration("Infrastructure:Persistence:Postgres"))
                .WithMigrationsFrom(typeof(IAssemblyMarker).Assembly)
                .WithDataSourcePlugin<MappingPlugin>()));

        collection.AddScoped<IPersistenceContext, PersistenceContext>();

        collection.AddSingleton<IBoardRepository, BoardRepository>();
        collection.AddSingleton<ITopicRepository, TopicRepository>();

        return collection;
    }
}