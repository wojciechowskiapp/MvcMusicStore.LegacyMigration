namespace MvcMusicStore.Application.Store.Queries;

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
/// Query for GenreMenu operation.
/// </summary>
public record StoreGenreMenuQuery : IRequest<Result>;

/// <summary>
/// Handles the StoreGenreMenuQuery query.
/// </summary>
public sealed class StoreGenreMenuHandler : IRequestHandler<StoreGenreMenuQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<StoreGenreMenuHandler> _logger;

    public StoreGenreMenuHandler(IApplicationDbContext context, ILogger<StoreGenreMenuHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(StoreGenreMenuQuery request, CancellationToken cancellationToken)
    {
        // Business logic from StoreController.GenreMenu
        var genres = await _context.Genres.ToListAsync(cancellationToken);
        return Result.Success();
    }
}
