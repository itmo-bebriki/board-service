using Grpc.Core;
using Grpc.Core.Interceptors;
using Itmo.Bebriki.Boards.Application.Contracts.Boards.Exceptions;
using Itmo.Bebriki.Boards.Application.Contracts.Topics.Exceptions;
using Microsoft.Extensions.Logging;

namespace Itmo.Bebriki.Boards.Presentation.Grpc.Interceptors;

public sealed class ExceptionHandlingInterceptor : Interceptor
{
    private readonly ILogger<ExceptionHandlingInterceptor> _logger;

    public ExceptionHandlingInterceptor(ILogger<ExceptionHandlingInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            throw HandleException(exception);
        }
    }

    private static RpcException HandleException(Exception exception)
    {
        return exception switch
        {
            BoardNotFoundException => new RpcException(new Status(StatusCode.NotFound, exception.Message)),
            TopicNotFoundException => new RpcException(new Status(StatusCode.NotFound, exception.Message)),
            ArgumentOutOfRangeException => new RpcException(new Status(StatusCode.InvalidArgument, exception.Message)),
            RpcException rpcException => rpcException,
            _ => new RpcException(new Status(StatusCode.Internal, "Internal Server Error")),
        };
    }
}