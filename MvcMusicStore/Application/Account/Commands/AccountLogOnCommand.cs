namespace MvcMusicStore.Application.Account.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

/// <summary>
/// Command for LogOn operation.
/// </summary>
public record AccountLogOnCommand(
    LogOnModel Model,
    string ReturnUrl
) : IRequest<Result>;

/// <summary>
/// Handles the AccountLogOnCommand command.
/// </summary>
public sealed class AccountLogOnHandler : IRequestHandler<AccountLogOnCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public AccountLogOnHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(AccountLogOnCommand request, CancellationToken cancellationToken)
    {
        // Business logic from AccountController.LogOn
        // TODO: Add validation (e.g., FluentValidation in request pipeline)
        if (Membership.ValidateUser(request.Model.UserName, request.Model.Password))
        {
            // TODO: Private method MigrateShoppingCart() is also used by: Register
            // Consider extracting to a shared helper class or domain service
            (// Associate shopping cart items with logged-in user
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.MigrateCart(request.Model.UserName);
            Session[ShoppingCart.CartSessionKey] = request.Model.UserName;);
            FormsAuthentication.SetAuthCookie(request.Model.UserName, request.Model.RememberMe);
            if (Url.IsLocalUrl(request.ReturnUrl) && request.ReturnUrl.Length > 1 && request.ReturnUrl.StartsWith("/") && !request.ReturnUrl.StartsWith("//") && !request.ReturnUrl.StartsWith("/\\"))
            {
                return Result.Success();
            }
            else
            {
                return Result.Success();
            }
        }
        else
        {
            return Result.Failure("The user name or password provided is incorrect.");
        }
        // Validation failure path removed - handled by pipeline validation
    }
}
