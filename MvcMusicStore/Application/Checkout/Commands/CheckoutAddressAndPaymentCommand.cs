namespace MvcMusicStore.Application.Checkout.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Command for AddressAndPayment operation.
/// Generated with 85% confidence from CheckoutController.AddressAndPayment.
/// </summary>
public record CheckoutAddressAndPaymentCommand(IFormCollection Values) : IRequest<Result>;

/// <summary>
/// Handles the CheckoutAddressAndPaymentCommand command.
/// </summary>
public sealed class CheckoutAddressAndPaymentHandler : IRequestHandler<CheckoutAddressAndPaymentCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CheckoutAddressAndPaymentHandler> _logger;
    private readonly ICurrentUserService _currentUser;
    private readonly IDateTime _dateTime;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CheckoutAddressAndPaymentHandler(
        IApplicationDbContext context,
        ILogger<CheckoutAddressAndPaymentHandler> logger,
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

    public async Task<Result> Handle(CheckoutAddressAndPaymentCommand request, CancellationToken cancellationToken)
    {
        // Business logic from CheckoutController.AddressAndPayment
        var order = new Order();
        await TryUpdateModelAsync(order);
        try
        {
            if (string.Equals(request.Values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
            {
                return View(order);
            }
            else
            {
                order.Username = _httpContextAccessor.HttpContext?.User?.Identity.Name;
                order.OrderDate = DateTime.Now;
                await                     //Save Order
                _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync(cancellationToken);
                //Process the order
                var cart = ShoppingCart.GetCart(_httpContextAccessor.HttpContext);
                cart.CreateOrder(order);
                // TODO: Was RedirectToAction("Complete", new { id = order.OrderId }) - use Result.Failure if this was error handling
                return Result.Success();
            }
        }
        catch
        {
            //Invalid - redisplay with errors
            return View(order);
        }
    }
}
