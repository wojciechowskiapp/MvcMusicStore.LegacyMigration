namespace MvcMusicStore.Application.ShoppingCart.Queries;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

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

    public ShoppingCartAddToCartHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(ShoppingCartAddToCartQuery request, CancellationToken cancellationToken)
    {
        // Business logic from ShoppingCartController.AddToCart
        var addedAlbum = _context.Albums.SingleAsync(album => album.AlbumId == request.Id);
        var cart = ShoppingCart.GetCart(_httpContextAccessor.HttpContext);
        cart.AddToCart(addedAlbum);
        return Result.Success();
    }
}
