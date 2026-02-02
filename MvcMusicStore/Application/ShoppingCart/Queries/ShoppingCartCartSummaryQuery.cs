namespace MvcMusicStore.Application.ShoppingCart.Queries;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

/// <summary>
/// Query for CartSummary operation.
/// Generated with 75% confidence from ShoppingCartController.CartSummary.
/// </summary>
public record ShoppingCartCartSummaryQuery : IRequest<Result>;

/// <summary>
/// Response DTO for ShoppingCartCartSummaryQuery containing ViewBag/ViewData properties.
/// </summary>
public record ShoppingCartCartSummaryResponseDto
{
    /// <summary>
    /// Gets or initializes CartCount.
    /// TODO: Review type inference - assigned value: cart.GetCount()
    /// </summary>
    public object? CartCount { get; init; }

}

/// <summary>
/// Handles the ShoppingCartCartSummaryQuery query.
/// TODO: Review implementation - generated with 75% confidence.
/// </summary>
public sealed class ShoppingCartCartSummaryHandler : IRequestHandler<ShoppingCartCartSummaryQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public ShoppingCartCartSummaryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(ShoppingCartCartSummaryQuery request, CancellationToken cancellationToken)
    {
        // Business logic from ShoppingCartController.CartSummary
        var result = new ShoppingCartCartSummaryResponseDto();

        var cart = ShoppingCart.GetCart(_httpContextAccessor.HttpContext);
        ViewData["CartCount"] = cart.GetCount();
        return Result.Success();
    }
}
