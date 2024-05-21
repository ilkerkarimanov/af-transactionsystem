using AF.TransactionSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AF.TransactionSystem.Infrastructure
{
    internal class AccountDbConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts", TransactionSystemDbContext.DEFAULT_SCHEMA);

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasConversion(
                value => value.Value,
                value => new AccountId(value));

            builder.OwnsOne(e => e.Name, nameBuilder => {
                nameBuilder.Property(r => r.FirstName).HasMaxLength(20).HasColumnName("FirstName");
                nameBuilder.Property(r => r.LastName).HasMaxLength(20).HasColumnName("LastName");
            });

            builder.Property(e => e.AccountNumber)
                .HasColumnName("AccountNumber")
                .HasConversion(
                    value => value.Value,
                    value => AccountNumber.Create(value));

            builder.HasMany(x => x.Credits)
                .WithOne()
                .HasForeignKey(x => x.AccountId);

            builder.HasMany(x => x.Debits)
                .WithOne()
                .HasForeignKey(x => x.AccountId);
        }
    }
}
