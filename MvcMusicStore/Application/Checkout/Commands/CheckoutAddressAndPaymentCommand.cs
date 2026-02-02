namespace MvcMusicStore.Application.Checkout.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

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

    public CheckoutAddressAndPaymentHandler(IApplicationDbContext context)
    {
        _context = context;
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
                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;
                //Save Order
                _context.Orders.AddAsync(order);
                _context.SaveChangesAsync(cancellationToken);
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
