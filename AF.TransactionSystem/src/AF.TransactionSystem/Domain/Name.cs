using Ardalis.GuardClauses;

namespace AF.TransactionSystem.Domain
{
    public record Name
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
    }
}
