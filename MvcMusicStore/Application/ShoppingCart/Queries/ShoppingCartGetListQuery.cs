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
/// Query for Index operation.
/// </summary>
public record ShoppingCartGetListQuery : IRequest<Result>;

/// <summary>
/// Handles the ShoppingCartGetListQuery query.
/// </summary>
public sealed class ShoppingCartGetListHandler : IRequestHandler<ShoppingCartGetListQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<ShoppingCartGetListHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ShoppingCartGetListHandler(
        IApplicationDbContext context,
        ILogger<ShoppingCartGetListHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
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
