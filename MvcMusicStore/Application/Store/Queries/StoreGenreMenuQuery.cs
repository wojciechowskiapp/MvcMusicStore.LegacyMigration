namespace MvcMusicStore.Application.Store.Queries;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

/// <summary>
/// Query for GenreMenu operation.
/// </summary>
public record StoreGenreMenuQuery : IRequest<Result>;

/// <summary>
/// Handles the StoreGenreMenuQuery query.
/// </summary>
public sealed class StoreGenreMenuHandler : IRequestHandler<StoreGenreMenuQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public StoreGenreMenuHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(StoreGenreMenuQuery request, CancellationToken cancellationToken)
    {
        // Business logic from StoreController.GenreMenu
        var genres = await _context.Genres.ToListAsync(cancellationToken);
        return Result.Success();
    }
}
