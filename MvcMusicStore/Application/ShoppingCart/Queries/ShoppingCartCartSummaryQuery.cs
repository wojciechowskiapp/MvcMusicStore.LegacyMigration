namespace MvcMusicStore.Application.ShoppingCart.Queries;

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Application.Common.Extensions;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;

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
    public object? CartCount { get; set; }

}

/// <summary>
/// Handles the ShoppingCartCartSummaryQuery query.
/// TODO: Review implementation - generated with 75% confidence.
/// </summary>
public sealed class ShoppingCartCartSummaryHandler : IRequestHandler<ShoppingCartCartSummaryQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<ShoppingCartCartSummaryHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ShoppingCartCartSummaryHandler(
        IApplicationDbContext context,
        ILogger<ShoppingCartCartSummaryHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<Result> Handle(ShoppingCartCartSummaryQuery request, CancellationToken cancellationToken)
    {
        // Business logic from ShoppingCartController.CartSummary
        var result = new ShoppingCartCartSummaryResponseDto();

        var cart = ShoppingCart.GetCart(_httpContextAccessor.HttpContext);
        result.CartCount = cart.GetCount();
        return Result.Success();
    }
}
