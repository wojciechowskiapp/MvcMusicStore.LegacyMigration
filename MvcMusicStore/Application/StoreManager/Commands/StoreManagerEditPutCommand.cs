namespace MvcMusicStore.Application.StoreManager.Commands;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

/// <summary>
/// Command for Edit operation.
/// Generated with 75% confidence from StoreManagerController.Edit.
/// </summary>
public record StoreManagerEditPutCommand(int Id) : IRequest<Result>;

/// <summary>
/// Response DTO for StoreManagerEditPutCommand containing ViewBag/ViewData properties.
/// </summary>
public record StoreManagerEditPutResponseDto
{
    /// <summary>
    /// Gets or initializes GenreId.
    /// </summary>
    public IEnumerable<SelectListItem> GenreId { get; init; }

    /// <summary>
    /// Gets or initializes ArtistId.
    /// </summary>
    public IEnumerable<SelectListItem> ArtistId { get; init; }

}

/// <summary>
/// Handles the StoreManagerEditPutCommand command.
/// TODO: Review implementation - generated with 75% confidence.
/// </summary>
public sealed class StoreManagerEditPutHandler : IRequestHandler<StoreManagerEditPutCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public StoreManagerEditPutHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(StoreManagerEditPutCommand request, CancellationToken cancellationToken)
    {
        // Business logic from StoreManagerController.Edit
        var result = new StoreManagerEditPutResponseDto();

        Album album = await _context.Albums.FindAsync(request.Id);
        result.GenreId = new SelectList(_context.Genres, "GenreId", "Name", album.GenreId);
        result.ArtistId = new SelectList(_context.Artists, "ArtistId", "Name", album.ArtistId);
        return Result.Success(album);
    }
}
