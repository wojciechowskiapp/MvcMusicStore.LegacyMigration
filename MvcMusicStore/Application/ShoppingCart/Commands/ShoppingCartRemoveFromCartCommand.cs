namespace MvcMusicStore.Application.ShoppingCart.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

/// <summary>
/// Command for RemoveFromCart operation.
/// </summary>
public record ShoppingCartRemoveFromCartCommand(int Id) : IRequest<Result>;

/// <summary>
/// Handles the ShoppingCartRemoveFromCartCommand command.
/// </summary>
public sealed class ShoppingCartRemoveFromCartHandler : IRequestHandler<ShoppingCartRemoveFromCartCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public ShoppingCartRemoveFromCartHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(ShoppingCartRemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        // Business logic from ShoppingCartController.RemoveFromCart
        var cart = ShoppingCart.GetCart(_httpContextAccessor.HttpContext);
        string albumName = _context.Carts.SingleAsync(item => item.RecordId == request.Id).Album.Title;
        int itemCount = cart.RemoveFromCart(request.Id);
        var results = new ShoppingCartRemoveViewModel
        {
            Message = System.Net.WebUtility.HtmlEncode(albumName) + " has been removed from your shopping cart.",
            CartTotal = cart.GetTotal(),
            CartCount = cart.GetCount(),
            ItemCount = itemCount,
            DeleteId = request.Id
        };
        return Result.Success(results);
    }
}
