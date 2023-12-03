using Microsoft.EntityFrameworkCore;

namespace AmarBari.Entities
{
    public class AmarBariDbContext : DbContext
    {
        public AmarBariDbContext(DbContextOptions options) : base(options) { }
        
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Flat> Flats { get; set; }
        public virtual DbSet<Renter> Renters { get; set; }     
        public virtual DbSet<MonthlyBill> MonthlyBills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Charities>()
            //    .Property(b => b.NumFavorites)
            //    .HasDefaultValue(0);

            //modelBuilder.Entity<CharityReviews>(entity =>
            //{
            //    entity.HasOne(d => d.Charity)
            //        .WithMany(p => p.CharityReviews)
            //        .HasForeignKey(d => d.CharityId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CharityReviews_Charity");

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.CharityReviews)
            //        .HasForeignKey(d => d.UserId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_CharityReviews_User");
            //});

            //modelBuilder.Entity<Favorites>(entity =>
            //{
            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.Favorites)
            //        .HasForeignKey(d => d.UserId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Favorites_User");

            //    entity.HasOne(d => d.Charity)
            //        .WithMany(p => p.Favorites)
            //        .HasForeignKey(d => d.CharityId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Favorites_Charity");
            //});
        }
    }
}
