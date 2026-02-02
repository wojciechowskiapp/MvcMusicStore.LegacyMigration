namespace MvcMusicStore.Application.Account.Queries;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

/// <summary>
/// Query for LogOff operation.
/// </summary>
public record AccountLogOffQuery : IRequest<Result>;

/// <summary>
/// Handles the AccountLogOffQuery query.
/// </summary>
public sealed class AccountLogOffHandler : IRequestHandler<AccountLogOffQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public AccountLogOffHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(AccountLogOffQuery request, CancellationToken cancellationToken)
    {
        // Business logic from AccountController.LogOff
        FormsAuthentication.SignOut();
        Session.Abandon();
        return Result.Success();
    }
}
