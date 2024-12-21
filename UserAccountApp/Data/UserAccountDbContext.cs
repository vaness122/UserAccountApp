using Microsoft.EntityFrameworkCore;
using UserAccountApp.Models;

namespace UserAccountApp.Data
{
    internal class UserAccountDbContext : DbContext
    {
        public UserAccountDbContext()
        {
        }

        public UserAccountDbContext(DbContextOptions<UserAccountDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-AOJDK8M;Database=UserAccountDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring 1-to-1 relationship between User and Address
            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId);
        }
    }
}
