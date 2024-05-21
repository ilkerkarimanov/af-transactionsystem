using AF.TransactionSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AF.TransactionSystem.Infrastructure
{
    internal class DebitDbConfiguration : IEntityTypeConfiguration<Debit>
    {
        public void Configure(EntityTypeBuilder<Debit> builder)
        {
            builder.ToTable("Debits", TransactionSystemDbContext.DEFAULT_SCHEMA);

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasConversion(
                value => value.Value,
                value => new DebitId(value));

            builder.Property(e => e.AccountId)
                .HasColumnName("AccountId")
                .HasConversion(
                    value => value.Value,
                    value => new AccountId(value));

            builder.Property(e => e.Amount)
                .HasColumnName("Amount")
                .HasConversion(
                    value => value.Amount,
                    value => new Money(value));

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("date2");
        }
    }
}
