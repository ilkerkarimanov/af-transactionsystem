
using Ardalis.GuardClauses;

namespace AF.TransactionSystem.Domain
{
    public record TransferId
    {
        public Guid Value { get; init; }
        private TransferId() { }
        public static TransferId Create(Guid value)
        {
            Guard.Against.InvalidInput(value, nameof(Value), (x) => x != Guid.Empty);
            return new TransferId() { Value = value };
        }
    }

    public class Transfer : Entity<TransferId>
    { 
        public Credit Credit { get; init; }
        public CreditId CreditId { get; private set; }
        public Debit Debit { get; init; }
        public DebitId DebitId { get; private set; }
        public DateTime CreatedDate { get; init; }

        private Transfer() { }

        public static Transfer Create(
            Debit debit,
            Credit credit
            )
        {
            return new Transfer()
            {
                Id = TransferId.Create(Guid.NewGuid()),
                Debit = debit,
                DebitId = debit.Id,
                Credit = credit,
                CreditId = credit.Id,
                CreatedDate = DateTime.UtcNow
            };
        }
    }
}
