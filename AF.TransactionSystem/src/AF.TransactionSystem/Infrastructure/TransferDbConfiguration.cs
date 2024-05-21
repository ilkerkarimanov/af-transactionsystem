using AF.TransactionSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AF.TransactionSystem.Infrastructure
{
    internal class TransferDbConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("Transfers", TransactionSystemDbContext.DEFAULT_SCHEMA);

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasConversion(
                value => value.Value,
                value => new TransferId(value));

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("date2");

            builder.HasOne(x => x.Debit)
                .WithOne()
                .HasForeignKey<Transfer>(x => x.DebitId);

            builder.HasOne(x => x.Credit)
                .WithOne()
                .HasForeignKey<Transfer>(x => x.CreditId);
        }
    }
}
