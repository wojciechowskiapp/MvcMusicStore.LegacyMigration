namespace MvcMusicStore.Application.StoreManager.Queries;

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
public record StoreManagerGetByIdQuery(int Id) : IRequest<Result>;

/// <summary>
/// Handles the StoreManagerGetByIdQuery query.
/// </summary>
public sealed class StoreManagerGetByIdHandler : IRequestHandler<StoreManagerGetByIdQuery, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<StoreManagerGetByIdHandler> _logger;

    public StoreManagerGetByIdHandler(IApplicationDbContext context, ILogger<StoreManagerGetByIdHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(StoreManagerGetByIdQuery request, CancellationToken cancellationToken)
    {
        // Business logic from StoreManagerController.Details
        Album album = await _context.Albums.FindAsync(request.Id);
        return Result.Success(album);
    }
}
