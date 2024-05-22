using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace AF.TransactionSystem.Domain
{
    public readonly record struct CreditId
    {
        public Guid Value { get; init; }
        public CreditId() { Value = Guid.NewGuid(); }

        public CreditId(Guid value)
        {
            Guard.Against.InvalidInput(value, nameof(Value), (x) => x != Guid.Empty);
            Value = value;
        }
    }

    public class Credit: EntityBase<CreditId>
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
                Id = new CreditId(),
                AccountId = accountId,
                Amount = amount,
                CreatedDate = DateTime.UtcNow
            };
        }
    }

    public class CreditOperationException : Exception
    {
        public CreditOperationException(string accountNumber, decimal amount, string reason, Exception ex)
            : base($"Credit operation didn't complete. AccountNumber: {accountNumber}, Amount: {amount}, Reason: {reason}", ex)
        { }
    }
}
