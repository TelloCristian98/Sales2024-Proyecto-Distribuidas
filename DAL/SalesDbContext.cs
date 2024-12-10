using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map entity to lowercase table name in PostgreSQL
            modelBuilder.Entity<Products>().ToTable("products");
            modelBuilder.Entity<Categories>().ToTable("categories");

            // Configure primary keys (if not already done)
            modelBuilder.Entity<Products>().HasKey(p => p.productid);
            modelBuilder.Entity<Categories>().HasKey(c => c.categoryid);
        }
    }
}