using AF.TransactionSystem.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AF.TransactionSystem.Infrastructure
{
    public class TransactionSystemDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "transactionsystem";

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Debit> Debits { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        public TransactionSystemDbContext(DbContextOptions<TransactionSystemDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
