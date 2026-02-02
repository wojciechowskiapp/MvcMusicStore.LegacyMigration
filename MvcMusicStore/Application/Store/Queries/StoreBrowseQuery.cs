namespace MvcMusicStore.Application.Store.Queries;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

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

    public StoreBrowseHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Result> Handle(StoreBrowseQuery request, CancellationToken cancellationToken)
    {
        // Business logic from StoreController.Browse
        var genreModel = _context.Genres.Include("Albums").SingleAsync(g => g.Name == request.Genre);
        return Result.Success(genreModel);
    }
}
