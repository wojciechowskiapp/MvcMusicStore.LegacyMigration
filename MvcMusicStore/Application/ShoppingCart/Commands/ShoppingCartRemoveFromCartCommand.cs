namespace MvcMusicStore.Application.ShoppingCart.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;

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
    private readonly ILogger<ShoppingCartRemoveFromCartHandler> _logger;
    private readonly ICurrentUserService _currentUser;
    private readonly IDateTime _dateTime;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ShoppingCartRemoveFromCartHandler(
        IApplicationDbContext context,
        ILogger<ShoppingCartRemoveFromCartHandler> logger,
        ICurrentUserService currentUser,
        IDateTime dateTime,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _logger = logger;
        _currentUser = currentUser;
        _dateTime = dateTime;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result> Handle(ShoppingCartRemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        // Business logic from ShoppingCartController.RemoveFromCart
        var cart = ShoppingCart.GetCart(_httpContextAccessor.HttpContext);
        string albumName = await _context.Carts.SingleAsync(item => item.RecordId == request.Id).Album.Title;
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
