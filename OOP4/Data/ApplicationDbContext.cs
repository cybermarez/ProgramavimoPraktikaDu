using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categorys { get; set; }

        public DbSet<UserComment> UserComments { get; set; }

        public DbSet<WishList> WishLists { get; set; }

        public DbSet<ProductCart> ProductCarts { get; set; }

        public DbSet<PurchaseHistory> PurchaseHistorys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=AsTaveMyliu;trusted_connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
