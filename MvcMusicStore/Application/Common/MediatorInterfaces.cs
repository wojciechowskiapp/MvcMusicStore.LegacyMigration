namespace MvcMusicStore.Application.Common;

using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Marker interface for requests that return a response.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IRequest<out TResponse>
{
}

/// <summary>
/// Marker interface for requests that don't return a response.
/// </summary>
public interface IRequest : IRequest<Unit>
{
}

/// <summary>
/// Defines a handler for a request.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handles the request.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response.</returns>
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

/// <summary>
/// Defines a handler for a request that doesn't return a response.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
public interface IRequestHandler<in TRequest>
    where TRequest : IRequest<Unit>
{
    /// <summary>
    /// Handles the request.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Unit value.</returns>
    Task<Unit> Handle(TRequest request, CancellationToken cancellationToken);
}

/// <summary>
/// Represents a void type. Used when a handler doesn't return data.
/// </summary>
public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>, IComparable
{
    /// <summary>
    /// Gets the default Unit value.
    /// </summary>
    public static readonly Unit Value = new();

    /// <inheritdoc />
    public bool Equals(Unit other) => true;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Unit;

    /// <inheritdoc />
    public override int GetHashCode() => 0;

    /// <inheritdoc />
    public int CompareTo(Unit other) => 0;

    /// <inheritdoc />
    public int CompareTo(object? obj) => 0;

    /// <summary>
    /// Equality operator.
    /// </summary>
    public static bool operator ==(Unit left, Unit right) => true;

    /// <summary>
    /// Inequality operator.
    /// </summary>
    public static bool operator !=(Unit left, Unit right) => false;

    /// <inheritdoc />
    public override string ToString() => "()";
}
