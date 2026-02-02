namespace MvcMusicStore.Application.StoreManager.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;

/// <summary>
/// Command for Delete operation.
/// </summary>
public record StoreManagerDeleteCommand(int Id) : IRequest<Result>;

/// <summary>
/// Handles the StoreManagerDeleteCommand command.
/// </summary>
public sealed class StoreManagerDeleteHandler : IRequestHandler<StoreManagerDeleteCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public StoreManagerDeleteHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(StoreManagerDeleteCommand request, CancellationToken cancellationToken)
    {
        // Business logic from StoreManagerController.Delete
        Album album = await _context.Albums.FindAsync(request.Id);
        return Result.Success(album);
    }
}
