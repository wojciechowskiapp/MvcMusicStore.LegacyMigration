namespace MvcMusicStore.Application.StoreManager.Queries;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

/// <summary>
/// Query for Index operation.
/// </summary>
public record StoreManagerGetListQuery : IRequest<Result>;

/// <summary>
/// Handles the StoreManagerGetListQuery query.
/// </summary>
public sealed class StoreManagerGetListHandler : IRequestHandler<StoreManagerGetListQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public StoreManagerGetListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(StoreManagerGetListQuery request, CancellationToken cancellationToken)
    {
        // Business logic from StoreManagerController.Index
        var albums = _context.Albums.Include(a => a.Genre).Include(a => a.Artist);
        return Result.Success(albums.ToList());
    }
}
