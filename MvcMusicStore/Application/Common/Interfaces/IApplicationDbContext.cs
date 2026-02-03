namespace MvcMusicStore.Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MvcMusicStore.Models;

/// <summary>
/// Defines the application database context contract for dependency injection.
/// Implemented by the infrastructure layer's DbContext.
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Gets the DbSet for Album entities.
    /// </summary>
    DbSet<Album> Albums { get; }

    /// <summary>
    /// Gets the DbSet for Artist entities.
    /// </summary>
    DbSet<Artist> Artists { get; }

    /// <summary>
    /// Gets the DbSet for Cart entities.
    /// </summary>
    DbSet<Cart> Carts { get; }

    /// <summary>
    /// Gets the DbSet for Genre entities.
    /// </summary>
    DbSet<Genre> Genres { get; }

    /// <summary>
    /// Gets the DbSet for MusicStoreEntities entities.
    /// </summary>
    DbSet<MusicStoreEntities> MusicStoreEntitieses { get; }

    /// <summary>
    /// Gets the DbSet for Order entities.
    /// </summary>
    DbSet<Order> Orders { get; }

    /// <summary>
    /// Gets the DbSet for OrderDetail entities.
    /// </summary>
    DbSet<OrderDetail> OrderDetails { get; }

    /// <summary>
    /// Gets the DbSet for ShoppingCart entities.
    /// </summary>
    DbSet<ShoppingCart> ShoppingCarts { get; }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
