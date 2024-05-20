using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace AF.TransactionSystem.Domain
{
    public class AccountNumber : ValueObject
    {
        public string Value { get; init; }
        private AccountNumber() { }
        public static AccountNumber Create(string value)
        {
            Guard.Against.NullOrEmpty(value, nameof(Value));
            return new AccountNumber() { Value = value };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
