namespace MvcMusicStore.Application.Checkout.Queries;

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
using Microsoft.AspNetCore.Http;

/// <summary>
/// Query for Complete operation.
/// </summary>
public record CheckoutCompleteQuery(int Id) : IRequest<Result>;

/// <summary>
/// Handles the CheckoutCompleteQuery query.
/// </summary>
public sealed class CheckoutCompleteHandler : IRequestHandler<CheckoutCompleteQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CheckoutCompleteHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CheckoutCompleteHandler(
        IApplicationDbContext context,
        ILogger<CheckoutCompleteHandler> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result> Handle(CheckoutCompleteQuery request, CancellationToken cancellationToken)
    {
        // Business logic from CheckoutController.Complete
        bool isValid = await _context.Orders.AnyAsync(o => o.OrderId == request.Id && o.Username == _httpContextAccessor.HttpContext?.User?.Identity.Name);
        if (isValid)
        {
            return Result.Success(request.Id);
        }
        else
        {
            return Result.Success();
        }
    }
}
