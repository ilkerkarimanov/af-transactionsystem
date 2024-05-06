using Ardalis.GuardClauses;

namespace AF.TransactionSystem.Domain
{
    public record CreditId
    {
        public Guid Value { get; init; }
        private CreditId() { }
        public static CreditId Create(Guid value)
        {
            Guard.Against.InvalidInput(value, nameof(Value), (x) => x != Guid.Empty);
            return new CreditId() { Value = value };
        }
    }

    public class Credit: Entity<CreditId>
    {
        public AccountId AccountId { get; init; }
        public Money Amount { get; init; }
        public DateTime CreatedDate { get; init; }
        private Credit() { }
        public static Credit Create(AccountId accountId, Money amount)
        {
            Guard.Against.NegativeOrZero(amount.Amount, nameof(Amount));

            return new Credit()
            {
                Id = CreditId.Create(Guid.NewGuid()),
                AccountId = accountId,
                Amount = amount,
                CreatedDate = DateTime.UtcNow
            };
        }

        public override bool Equals(object obj) => obj is Credit credit && Id == credit.Id;
        public override int GetHashCode() => HashCode.Combine(Id);
    }
}
