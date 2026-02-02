namespace MvcMusicStore.Application.Home.Queries;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

/// <summary>
/// Query for Index operation.
/// </summary>
public record HomeGetListQuery : IRequest<Result>;

/// <summary>
/// Handles the HomeGetListQuery query.
/// </summary>
public sealed class HomeGetListHandler : IRequestHandler<HomeGetListQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public HomeGetListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(HomeGetListQuery request, CancellationToken cancellationToken)
    {
        // Business logic from HomeController.Index
        var albums = (// Group the order details by album and return
        // the albums with the highest 5
        return _context.Albums.OrderByDescending(a => a.OrderDetails.Count()).Take(5).ToList(););
        return Result.Success(albums);
    }
}
