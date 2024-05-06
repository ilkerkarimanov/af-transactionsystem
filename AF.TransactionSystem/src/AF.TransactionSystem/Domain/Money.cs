namespace AF.TransactionSystem.Domain
{
    public record Money(decimal Amount)
    {
        public Money Add(Money value)  => new Money(Amount + value.Amount);

        public Money Subtract(Money value) => new Money(Amount - value.Amount);

        public bool Positive() => Amount > 0;

        public bool Zero() => Amount == 0;

        public bool Negative() => Amount < 0;
    }
}
