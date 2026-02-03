using Microsoft.EntityFrameworkCore;

namespace MvcMusicStore.Models
{
    public class MusicStoreEntities : DbContext
    {

        public MusicStoreEntities(DbContextOptions<MusicStoreEntities> options) : base(options)
        {
            // TODO: EF6 had lazy loading enabled by default
            // To enable in EF Core, add to Program.cs:
            // services.AddDbContext<MusicStoreEntities>(options =>
            //     options.UseLazyLoadingProxies()
            //            .UseSqlServer(connectionString));
            // Requires: Microsoft.EntityFrameworkCore.Proxies package
            // Note: Navigation properties must be virtual
        }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}