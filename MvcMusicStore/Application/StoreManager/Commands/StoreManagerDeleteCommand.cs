namespace MvcMusicStore.Application.StoreManager.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;

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
    private readonly ILogger<StoreManagerDeleteHandler> _logger;
    private readonly ICurrentUserService _currentUser;
    private readonly IDateTime _dateTime;

    public StoreManagerDeleteHandler(
        IApplicationDbContext context,
        ILogger<StoreManagerDeleteHandler> logger,
        ICurrentUserService currentUser,
        IDateTime dateTime)
    {
        _context = context;
        _logger = logger;
        _currentUser = currentUser;
        _dateTime = dateTime;
    }

    public async Task<Result> Handle(StoreManagerDeleteCommand request, CancellationToken cancellationToken)
    {
        // Business logic from StoreManagerController.Delete
        Album album = await _context.Albums.FindAsync(request.Id);
        return Result.Success(album);
    }
}
