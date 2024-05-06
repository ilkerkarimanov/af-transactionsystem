using Ardalis.GuardClauses;

namespace AF.TransactionSystem.Domain
{
    public record AccountNumber
    {
        public string Value { get; init; }
        private AccountNumber() { }
        public static AccountNumber Create(string value)
        {
            Guard.Against.NullOrEmpty(value, nameof(Value));
            return new AccountNumber() { Value = value };
        }
    }
}
