using BankSystem.PaymentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.PaymentSystem.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PaymentCard> PaymentCards { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
    }
}
