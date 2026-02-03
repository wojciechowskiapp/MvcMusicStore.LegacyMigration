namespace MvcMusicStore.Application.Account.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;

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
    private readonly ILogger<AccountLogOnHandler> _logger;
    private readonly ICurrentUserService _currentUser;
    private readonly IDateTime _dateTime;

    public AccountLogOnHandler(
        IApplicationDbContext context,
        ILogger<AccountLogOnHandler> logger,
        ICurrentUserService currentUser,
        IDateTime dateTime)
    {
        _context = context;
        _logger = logger;
        _currentUser = currentUser;
        _dateTime = dateTime;
    }

    public Task<Result> Handle(AccountLogOnCommand request, CancellationToken cancellationToken)
    {
        // Business logic from AccountController.LogOn
        // TODO: Add validation (e.g., FluentValidation in request pipeline)
        if (Membership.ValidateUser(request.Model.UserName, request.Model.Password))
        {
            MigrateShoppingCart(request.Model.UserName);
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

    /// <summary>
    /// Private helper method migrated from controller.
    /// TODO: Review and adapt as needed for handler context.
    /// </summary>
    private void MigrateShoppingCart(string UserName)
    {
        // Associate shopping cart items with logged-in user
        var cart = ShoppingCart.GetCart(_httpContextAccessor.HttpContext);
        cart.MigrateCart(UserName);
        Session[ShoppingCart.CartSessionKey] = UserName;
    }
}
