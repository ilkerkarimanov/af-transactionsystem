using Ardalis.GuardClauses;

namespace AF.TransactionSystem.Domain
{
    public record DebitId
    {
        public Guid Value { get; init; }
        private DebitId() { }
        public static DebitId Create(Guid value) 
        {
            Guard.Against.InvalidInput(value, nameof(Value), (x) => x != Guid.Empty);
            return new DebitId() { Value = value };
        }
    }

    public class Debit : Entity<DebitId>
    {
        public AccountId AccountNumber { get; init; }
        public Money Amount { get; init; }
        public DateTime CreatedDate { get; init; }
        private Debit() { }
        public static Debit Create(AccountId accountNumber, Money amount)
        {

            Guard.Against.NegativeOrZero(amount.Amount, nameof(Amount));

            return new Debit()
            {
                Id = DebitId.Create(Guid.NewGuid()),
                AccountNumber = accountNumber,
                Amount = amount,
                CreatedDate = DateTime.UtcNow
            };
        }

        public override bool Equals(object obj) => obj is Debit credit && Id == credit.Id;
        public override int GetHashCode() => HashCode.Combine(Id);
    }
}
