using Itmo.Bebriki.Boards.Application.Boards;
using Itmo.Bebriki.Boards.Application.Contracts.Boards;
using Itmo.Bebriki.Boards.Application.Contracts.Topics;
using Itmo.Bebriki.Boards.Application.Topics;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.Bebriki.Boards.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IBoardService, BoardService>();
        collection.AddScoped<ITopicService, TopicService>();

        return collection;
    }
}