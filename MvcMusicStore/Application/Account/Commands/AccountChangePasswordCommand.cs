namespace MvcMusicStore.Application.Account.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

/// <summary>
/// Command for ChangePassword operation.
/// Generated with 85% confidence from AccountController.ChangePassword.
/// </summary>
public record AccountChangePasswordCommand(ChangePasswordModel Model) : IRequest<Result>;

/// <summary>
/// Handles the AccountChangePasswordCommand command.
/// </summary>
public sealed class AccountChangePasswordHandler : IRequestHandler<AccountChangePasswordCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public AccountChangePasswordHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(AccountChangePasswordCommand request, CancellationToken cancellationToken)
    {
        // Business logic from AccountController.ChangePassword
        // TODO: Add validation (e.g., FluentValidation in request pipeline)
        bool changePasswordSucceeded;
        try
        {
            MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
            changePasswordSucceeded = currentUser.ChangePassword(request.Model.OldPassword, request.Model.NewPassword);
        }
        catch (Exception)
        {
            changePasswordSucceeded = false;
        }
        if (changePasswordSucceeded)
        {
            return Result.Success();
        }
        else
        {
            return Result.Failure("The current password is incorrect or the new password is invalid.");
        }
        // Validation failure path removed - handled by pipeline validation
    }
}
