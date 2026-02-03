namespace MvcMusicStore.Application.Account.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

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
    private readonly ILogger<AccountChangePasswordHandler> _logger;
    private readonly ICurrentUserService _currentUser;
    private readonly IDateTime _dateTime;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountChangePasswordHandler(
        IApplicationDbContext context,
        ILogger<AccountChangePasswordHandler> logger,
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

    public Task<Result> Handle(AccountChangePasswordCommand request, CancellationToken cancellationToken)
    {
        // Business logic from AccountController.ChangePassword
        // TODO: Add validation (e.g., FluentValidation in request pipeline)
        bool changePasswordSucceeded;
        try
        {
            MembershipUser currentUser = Membership.GetUser(_httpContextAccessor.HttpContext?.User?.Identity.Name, true /* userIsOnline */);
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
