namespace MvcMusicStore.Application.Account.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;

/// <summary>
/// Command for Register operation.
/// </summary>
public record AccountRegisterCommand(RegisterModel Model) : IRequest<Result>;

/// <summary>
/// Handles the AccountRegisterCommand command.
/// </summary>
public sealed class AccountRegisterHandler : IRequestHandler<AccountRegisterCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<AccountRegisterHandler> _logger;
    private readonly ICurrentUserService _currentUser;
    private readonly IDateTime _dateTime;

    public AccountRegisterHandler(
        IApplicationDbContext context,
        ILogger<AccountRegisterHandler> logger,
        ICurrentUserService currentUser,
        IDateTime dateTime)
    {
        _context = context;
        _logger = logger;
        _currentUser = currentUser;
        _dateTime = dateTime;
    }

    public Task<Result> Handle(AccountRegisterCommand request, CancellationToken cancellationToken)
    {
        // Business logic from AccountController.Register
        // TODO: Add validation (e.g., FluentValidation in request pipeline)
        MembershipCreateStatus createStatus;
        Membership.CreateUser(request.Model.UserName, request.Model.Password, request.Model.Email, "question", "answer", true, null, out createStatus);
        if (createStatus == /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.Success)
        {
            MigrateShoppingCart(request.Model.UserName);
            FormsAuthentication.SetAuthCookie(request.Model.UserName, false /* createPersistentCookie */);
            return Result.Success();
        }
        else
        {
            ModelState.AddModelError("", ErrorCodeToString(createStatus));
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

    /// <summary>
    /// Private helper method migrated from controller.
    /// TODO: Review and adapt as needed for handler context.
    /// </summary>
    private static string ErrorCodeToString(/* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus createStatus)
    {
        // See http://go.microsoft.com/fwlink/?LinkID=177550 for
        // a full list of status codes.
        switch (createStatus)
        {
            case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.DuplicateUserName:
                return "User name already exists. Please enter a different user name.";
            case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.DuplicateEmail:
                return "A user name for that e-mail address already exists. Please enter a different e-mail address.";
            case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.InvalidPassword:
                return "The password provided is invalid. Please enter a valid password value.";
            case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.InvalidEmail:
                return "The e-mail address provided is invalid. Please check the value and try again.";
            case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.InvalidAnswer:
                return "The password retrieval answer provided is invalid. Please check the value and try again.";
            case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.InvalidQuestion:
                return "The password retrieval question provided is invalid. Please check the value and try again.";
            case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.InvalidUserName:
                return "The user name provided is invalid. Please check the value and try again.";
            case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.ProviderError:
                return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            case /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.UserRejected:
                return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            default:
                return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
        }
    }
}
