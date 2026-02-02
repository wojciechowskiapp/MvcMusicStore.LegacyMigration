namespace MvcMusicStore.Application.Account.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

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

    public AccountRegisterHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(AccountRegisterCommand request, CancellationToken cancellationToken)
    {
        // Business logic from AccountController.Register
        // TODO: Add validation (e.g., FluentValidation in request pipeline)
        MembershipCreateStatus createStatus;
        Membership.CreateUser(request.Model.UserName, request.Model.Password, request.Model.Email, "question", "answer", true, null, out createStatus);
        if (createStatus == /* TODO: Replace MembershipCreateStatus with IdentityResult from ASP.NET Core Identity */ MembershipCreateStatus.Success)
        {
            // TODO: Private method MigrateShoppingCart() is also used by: Register
            // Consider extracting to a shared helper class or domain service
            (// Associate shopping cart items with logged-in user
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.MigrateCart(request.Model.UserName);
            Session[ShoppingCart.CartSessionKey] = request.Model.UserName;);
            FormsAuthentication.SetAuthCookie(request.Model.UserName, false /* createPersistentCookie */);
            return Result.Success();
        }
        else
        {
            ModelState.AddModelError("", (// See http://go.microsoft.com/fwlink/?LinkID=177550 for
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
            }));
        }
        // Validation failure path removed - handled by pipeline validation
    }
}
