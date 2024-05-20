using Ardalis.SharedKernel;

namespace AF.TransactionSystem.Domain
{
    public class Money : ValueObject
    {
        public Money(decimal amount)
        {
            Amount = amount;
        }
        public decimal Amount { get; init; }

        public Money Add(Money value)  => new Money(Amount + value.Amount);

        public Money Subtract(Money value) => new Money(Amount - value.Amount);

        public bool Positive() => Amount > 0;

        public bool Zero() => Amount == 0;

        public bool Negative() => Amount < 0;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
        }
    }
}
