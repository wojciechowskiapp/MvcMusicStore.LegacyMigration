namespace MvcMusicStore.Application.StoreManager.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;

/// <summary>
/// Command for DeleteConfirmed operation.
/// </summary>
public record StoreManagerDeleteConfirmedCommand(int Id) : IRequest<Result>;

/// <summary>
/// Handles the StoreManagerDeleteConfirmedCommand command.
/// </summary>
public sealed class StoreManagerDeleteConfirmedHandler : IRequestHandler<StoreManagerDeleteConfirmedCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<StoreManagerDeleteConfirmedHandler> _logger;
    private readonly ICurrentUserService _currentUser;
    private readonly IDateTime _dateTime;

    public StoreManagerDeleteConfirmedHandler(
        IApplicationDbContext context,
        ILogger<StoreManagerDeleteConfirmedHandler> logger,
        ICurrentUserService currentUser,
        IDateTime dateTime)
    {
        _context = context;
        _logger = logger;
        _currentUser = currentUser;
        _dateTime = dateTime;
    }

    public async Task<Result> Handle(StoreManagerDeleteConfirmedCommand request, CancellationToken cancellationToken)
    {
        // Business logic from StoreManagerController.DeleteConfirmed
        Album album = await _context.Albums.FindAsync(request.Id);
        _context.Albums.Remove(album);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
