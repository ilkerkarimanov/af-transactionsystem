using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace AF.TransactionSystem.Domain
{
    public class Name : ValueObject
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }

        private Name() { }

        public static Name Create(string firstName, string lastName)
        {
            Guard.Against.NullOrEmpty(firstName, nameof(FirstName));
            Guard.Against.NullOrEmpty(lastName, nameof(LastName));

            return new Name()
            {
                FirstName = firstName,
                LastName = lastName
            };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
