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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Query for AddToCart operation.
/// </summary>
public record ShoppingCartAddToCartQuery(int Id) : IRequest<Result>;

/// <summary>
/// Handles the ShoppingCartAddToCartQuery query.
/// </summary>
public sealed class ShoppingCartAddToCartHandler : IRequestHandler<ShoppingCartAddToCartQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<ShoppingCartAddToCartHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ShoppingCartAddToCartHandler(
        IApplicationDbContext context,
        ILogger<ShoppingCartAddToCartHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result> Handle(ShoppingCartAddToCartQuery request, CancellationToken cancellationToken)
    {
        // Business logic from ShoppingCartController.AddToCart
        var addedAlbum = await _context.Albums.SingleAsync(album => album.AlbumId == request.Id);
        var cart = ShoppingCart.GetCart(_httpContextAccessor.HttpContext);
        cart.AddToCart(addedAlbum);
        return Result.Success();
    }
}
