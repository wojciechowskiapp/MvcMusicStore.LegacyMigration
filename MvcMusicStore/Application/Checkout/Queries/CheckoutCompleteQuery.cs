namespace MvcMusicStore.Application.Checkout.Queries;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

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

    public CheckoutCompleteHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(CheckoutCompleteQuery request, CancellationToken cancellationToken)
    {
        // Business logic from CheckoutController.Complete
        bool isValid = _context.Orders.AnyAsync(o => o.OrderId == request.Id && o.Username == User.Identity.Name);
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
