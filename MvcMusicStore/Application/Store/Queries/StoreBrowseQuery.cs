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
/// Query for Browse operation.
/// </summary>
public record StoreBrowseQuery(string Genre) : IRequest<Result>;

/// <summary>
/// Handles the StoreBrowseQuery query.
/// </summary>
public sealed class StoreBrowseHandler : IRequestHandler<StoreBrowseQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<StoreBrowseHandler> _logger;

    public StoreBrowseHandler(IApplicationDbContext context, ILogger<StoreBrowseHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(StoreBrowseQuery request, CancellationToken cancellationToken)
    {
        // Business logic from StoreController.Browse
        var genreModel = await _context.Genres.Include("Albums").SingleAsync(g => g.Name == request.Genre);
        return Result.Success(genreModel);
    }
}
