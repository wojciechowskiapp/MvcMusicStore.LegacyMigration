namespace MvcMusicStore.Application.ShoppingCart.Queries;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

/// <summary>
/// Query for Index operation.
/// </summary>
public record ShoppingCartGetListQuery : IRequest<Result>;

/// <summary>
/// Handles the ShoppingCartGetListQuery query.
/// </summary>
public sealed class ShoppingCartGetListHandler : IRequestHandler<ShoppingCartGetListQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public ShoppingCartGetListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(ShoppingCartGetListQuery request, CancellationToken cancellationToken)
    {
        // Business logic from ShoppingCartController.Index
        var cart = ShoppingCart.GetCart(_httpContextAccessor.HttpContext);
        var viewModel = new ShoppingCartViewModel
        {
            CartItems = cart.GetCartItems(),
            CartTotal = cart.GetTotal()
        };
        return Result.Success(viewModel);
    }
}
