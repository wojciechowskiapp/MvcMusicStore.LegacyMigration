namespace MvcMusicStore.Application.Store.Queries;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

/// <summary>
/// Query for Index operation.
/// </summary>
public record StoreGetListQuery : IRequest<Result>;

/// <summary>
/// Handles the StoreGetListQuery query.
/// </summary>
public sealed class StoreGetListHandler : IRequestHandler<StoreGetListQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public StoreGetListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(StoreGetListQuery request, CancellationToken cancellationToken)
    {
        // Business logic from StoreController.Index
        var genres = await _context.Genres.ToListAsync(cancellationToken);
        return Result.Success(genres);
    }
}
