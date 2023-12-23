using BankSystem.CustomerInformation.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.CustomerInformation.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerInfos>()
                .HasKey(e => e.CustomerID); // Assuming Id is the primary key

            modelBuilder.Entity<CustomerInfos>()
                .Property(e => e.CustomerID)
                .ValueGeneratedOnAdd(); // This sets the Id as auto-incrementing

            modelBuilder.Entity<AccountInfos>()
            .Property(a => a.Balance)
            .HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed


            modelBuilder.Entity<AccountInfos>()
               .HasKey(e => e.AccountNumber); // Assuming Id is the primary key

            modelBuilder.Entity<AccountInfos>()
                .Property(e => e.AccountNumber)
                .ValueGeneratedOnAdd(); // This sets the Id as auto-incrementing
        }
        public DbSet<CustomerInfos> CustomerInfo { get; set; }
        public DbSet<AccountInfos> AccountInfo { get; set; }

    }
}
