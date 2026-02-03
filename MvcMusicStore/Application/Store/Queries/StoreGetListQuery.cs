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
/// Query for Index operation.
/// </summary>
public record StoreGetListQuery : IRequest<Result>;

/// <summary>
/// Handles the StoreGetListQuery query.
/// </summary>
public sealed class StoreGetListHandler : IRequestHandler<StoreGetListQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<StoreGetListHandler> _logger;

    public StoreGetListHandler(IApplicationDbContext context, ILogger<StoreGetListHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(StoreGetListQuery request, CancellationToken cancellationToken)
    {
        // Business logic from StoreController.Index
        var genres = await _context.Genres.ToListAsync(cancellationToken);
        return Result.Success(genres);
    }
}
