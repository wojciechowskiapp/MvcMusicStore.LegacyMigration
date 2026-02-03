namespace MvcMusicStore.Infrastructure;

using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;

/// <summary>
/// High-performance mediator implementation with cached delegates.
/// Resolves handlers from DI and caches compiled invoke delegates for performance.
/// </summary>
public sealed class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    // Cache for compiled handler invokers - avoids reflection on every call
    private static readonly ConcurrentDictionary<Type, HandlerInvokerInfo> _handlerCache = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Mediator"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider to resolve handlers.</param>
    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var requestType = request.GetType();
        var invokerInfo = _handlerCache.GetOrAdd(requestType, CreateHandlerInvoker<TResponse>);

        var handler = _serviceProvider.GetRequiredService(invokerInfo.HandlerType);
        return await ((Func<object, object, CancellationToken, Task<TResponse>>)invokerInfo.Invoker)(handler, request, cancellationToken);
    }

    /// <summary>
    /// Creates a compiled invoker delegate for a handler type.
    /// This is called once per request type and cached for subsequent calls.
    /// </summary>
    private static HandlerInvokerInfo CreateHandlerInvoker<TResponse>(Type requestType)
    {
        var responseType = typeof(TResponse);
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);

        // Get the Handle method
        var handleMethod = handlerType.GetMethod("Handle")!;

        // Build compiled expression: (handler, request, ct) => ((IRequestHandler<TReq, TRes>)handler).Handle((TReq)request, ct)
        var handlerParam = Expression.Parameter(typeof(object), "handler");
        var requestParam = Expression.Parameter(typeof(object), "request");
        var ctParam = Expression.Parameter(typeof(CancellationToken), "cancellationToken");

        var castHandler = Expression.Convert(handlerParam, handlerType);
        var castRequest = Expression.Convert(requestParam, requestType);
        var callHandle = Expression.Call(castHandler, handleMethod, castRequest, ctParam);

        var lambda = Expression.Lambda<Func<object, object, CancellationToken, Task<TResponse>>>(
            callHandle, handlerParam, requestParam, ctParam);

        return new HandlerInvokerInfo(handlerType, lambda.Compile());
    }

    /// <summary>
    /// Cached handler invoker information.
    /// </summary>
    private sealed record HandlerInvokerInfo(Type HandlerType, Delegate Invoker);
}
