using AmarBariAPI.Entities.Home;
using AmarBariAPI.Entities.Shop;
using Microsoft.EntityFrameworkCore;

namespace AmarBariAPI.Entities.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<HomeEntity> Homes { get; set; }
        public DbSet<FlatEntity> Flats { get; set; }
        public DbSet<FlatRenterEntity> FlatRenters { get; set; }

        public DbSet<ShopEntity> Shops { get; set; }
        public DbSet<ShopRenterEntity> ShopRenters { get; set; }
        public DbSet<ContractEntity> Contracts { get; set; }
    }
}
