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
        public DbSet<Users> Users { get; set; }
        public DbSet<AuditLogs> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map entity to lowercase table name in PostgreSQL
            modelBuilder.Entity<Products>().ToTable("products");
            modelBuilder.Entity<Categories>().ToTable("categories");
            modelBuilder.Entity<Users>().ToTable("users");
            modelBuilder.Entity<AuditLogs>().ToTable("audit_logs");

            // Configure primary keys (if not already done)
            modelBuilder.Entity<Products>().HasKey(p => p.productid);
            modelBuilder.Entity<Categories>().HasKey(c => c.categoryid);

            modelBuilder.Entity<Users>().ToTable("users");
            modelBuilder.Entity<Users>().Property(u => u.UserId).HasColumnName("userid");
            modelBuilder.Entity<Users>().Property(u => u.Username).HasColumnName("username");
            modelBuilder.Entity<Users>().Property(u => u.PasswordHash).HasColumnName("passwordhash");
            modelBuilder.Entity<Users>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<Users>().Property(u => u.Role).HasColumnName("role");
            modelBuilder.Entity<Users>().Property(u => u.IsActive).HasColumnName("isactive");
            modelBuilder.Entity<Users>().Property(u => u.CreatedAt).HasColumnName("createdat");
            modelBuilder.Entity<Users>().Property(u => u.UpdatedAt).HasColumnName("updatedat");

            modelBuilder.Entity<AuditLogs>().ToTable("audit_logs");
            modelBuilder.Entity<AuditLogs>().Property(a => a.LogId).HasColumnName("logid");
            modelBuilder.Entity<AuditLogs>().Property(a => a.UserId).HasColumnName("userid");
            modelBuilder.Entity<AuditLogs>().Property(a => a.Action).HasColumnName("action");
            modelBuilder.Entity<AuditLogs>().Property(a => a.IpAddress).HasColumnName("ipaddress");
            modelBuilder.Entity<AuditLogs>().Property(a => a.Timestamp).HasColumnName("timestamp");
        }
    }
}