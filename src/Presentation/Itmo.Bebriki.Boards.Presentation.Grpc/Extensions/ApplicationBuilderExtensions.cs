using Itmo.Bebriki.Boards.Presentation.Grpc.Controllers;
using Microsoft.AspNetCore.Builder;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UsePresentationGrpc(this IApplicationBuilder builder)
    {
        builder.UseEndpoints(routeBuilder =>
        {
            routeBuilder.MapGrpcService<BoardController>();
            routeBuilder.MapGrpcService<TopicController>();
            routeBuilder.MapGrpcReflectionService();
        });

        return builder;
    }
}