namespace MvcMusicStore.Application.Account.Queries;

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Application.Common.Extensions;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;

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
    private readonly ILogger<AccountLogOffHandler> _logger;

    public AccountLogOffHandler(IApplicationDbContext context, ILogger<AccountLogOffHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<Result> Handle(AccountLogOffQuery request, CancellationToken cancellationToken)
    {
        // Business logic from AccountController.LogOff
        FormsAuthentication.SignOut();
        Session.Abandon();
        return Result.Success();
    }
}
