namespace MvcMusicStore.Application.Common.Interfaces;

using MvcMusicStore.Application.Common;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Defines a mediator for sending requests to handlers.
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Sends a request to the appropriate handler.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="request">The request to send.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response from the handler.</returns>
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}
