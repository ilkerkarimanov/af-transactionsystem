using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace AF.TransactionSystem.Domain
{
    public readonly record struct DebitId
    {
        public Guid Value { get; init; }
        public DebitId() { Value = Guid.NewGuid(); }

        public DebitId(Guid value)
        {
            Guard.Against.InvalidInput(value, nameof(Value), (x) => x != Guid.Empty);
            Value = value;
        }
    }

    public class Debit : EntityBase<DebitId>
    {
        public AccountId AccountId { get; init; }
        public Money Amount { get; init; }
        public DateTime CreatedDate { get; init; }
        private Debit() { }
        public static Debit Create(AccountId accountNumber, Money amount)
        {

            Guard.Against.NegativeOrZero(amount.Amount, nameof(Amount));

            return new Debit()
            {
                Id = new DebitId(),
                AccountId = accountNumber,
                Amount = amount,
                CreatedDate = DateTime.UtcNow
            };
        }
    }

    public class DebitOperationException : Exception
    {
        public DebitOperationException(string accountNumber, decimal amount, string reason, Exception ex)
            : base($"Debit operation didn't complete. AccountNumber: {accountNumber}, Amount: {amount}, Reason: {reason}")
        { }
    }
}
