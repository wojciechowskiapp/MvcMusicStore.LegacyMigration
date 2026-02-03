namespace MvcMusicStore.Application.StoreManager.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

/// <summary>
/// Command for Create operation.
/// Generated with 75% confidence from StoreManagerController.Create.
/// </summary>
public record StoreManagerCreateCommand(Album Album) : IRequest<Result>;

/// <summary>
/// Response DTO for StoreManagerCreateCommand containing ViewBag/ViewData properties.
/// </summary>
public record StoreManagerCreateResponseDto
{
    /// <summary>
    /// Gets or initializes GenreId.
    /// </summary>
    public IEnumerable<SelectListItem> GenreId { get; set; }

    /// <summary>
    /// Gets or initializes ArtistId.
    /// </summary>
    public IEnumerable<SelectListItem> ArtistId { get; set; }

}

/// <summary>
/// Handles the StoreManagerCreateCommand command.
/// TODO: Review implementation - generated with 75% confidence.
/// </summary>
public sealed class StoreManagerCreateHandler : IRequestHandler<StoreManagerCreateCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<StoreManagerCreateHandler> _logger;
    private readonly ICurrentUserService _currentUser;
    private readonly IDateTime _dateTime;

    public StoreManagerCreateHandler(
        IApplicationDbContext context,
        ILogger<StoreManagerCreateHandler> logger,
        ICurrentUserService currentUser,
        IDateTime dateTime)
    {
        _context = context;
        _logger = logger;
        _currentUser = currentUser;
        _dateTime = dateTime;
    }

    public async Task<Result> Handle(StoreManagerCreateCommand request, CancellationToken cancellationToken)
    {
        // Business logic from StoreManagerController.Create
        var result = new StoreManagerCreateResponseDto();

        // TODO: Add validation (e.g., FluentValidation in request pipeline)
        await _context.Albums.AddAsync(request.Album);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
