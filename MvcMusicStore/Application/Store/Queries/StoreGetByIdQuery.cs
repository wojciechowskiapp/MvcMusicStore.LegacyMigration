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
/// Query for Details operation.
/// </summary>
public record StoreGetByIdQuery(int Id) : IRequest<Result>;

/// <summary>
/// Handles the StoreGetByIdQuery query.
/// </summary>
public sealed class StoreGetByIdHandler : IRequestHandler<StoreGetByIdQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<StoreGetByIdHandler> _logger;

    public StoreGetByIdHandler(IApplicationDbContext context, ILogger<StoreGetByIdHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(StoreGetByIdQuery request, CancellationToken cancellationToken)
    {
        // Business logic from StoreController.Details
        var album = await _context.Albums.FindAsync(request.Id);
        return Result.Success(album);
    }
}
