namespace MvcMusicStore.Application.Home.Queries;

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
/// Query for Index operation.
/// </summary>
public record HomeGetListQuery : IRequest<Result>;

/// <summary>
/// Handles the HomeGetListQuery query.
/// </summary>
public sealed class HomeGetListHandler : IRequestHandler<HomeGetListQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<HomeGetListHandler> _logger;

    public HomeGetListHandler(IApplicationDbContext context, ILogger<HomeGetListHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<Result> Handle(HomeGetListQuery request, CancellationToken cancellationToken)
    {
        // Business logic from HomeController.Index
        var albums = GetTopSellingAlbums(5);
        return Result.Success(albums);
    }

    /// <summary>
    /// Private helper method migrated from controller.
    /// TODO: Review and adapt as needed for handler context.
    /// </summary>
    private List<Album> GetTopSellingAlbums(int count)
    {
        // Group the order details by album and return
        // the albums with the highest count
        return _context.Albums.OrderByDescending(a => a.OrderDetails.Count()).Take(count).ToList();
    }
}
