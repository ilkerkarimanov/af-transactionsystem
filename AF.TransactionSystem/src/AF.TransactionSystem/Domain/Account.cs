using Ardalis.GuardClauses;

namespace AF.TransactionSystem.Domain
{
    public record AccountId
    {
        public Guid Value { get; init; }
        public AccountId() { Value = Guid.NewGuid(); }
        public static AccountId Create(Guid value)
        {
            Guard.Against.InvalidInput(value, nameof(Value), (x) => x != Guid.Empty);
            return new AccountId() { Value = value };
        }
    }

    public class Account : Entity<AccountId>
    {
        public Name Name { get; init; }
        public AccountNumber AccountNumber { get; init; }

        private readonly HashSet<Debit> _debits = new HashSet<Debit>();
        public IReadOnlyList<Debit> Debits => _debits.ToList();
        private HashSet<Credit> _credits = new HashSet<Credit>();
        public IReadOnlyList<Credit> Credits => _credits.ToList();

        private object _transactionLock = new object();

        public static Account Create(
            AccountNumber accountNumber,
            Name name)
        {
            var entity = new Account()
            {
                Id = new AccountId(),
                AccountNumber = accountNumber,
                Name = name
            };

            return entity;
        }

        public Credit Deposit(Money amount)
        {
            lock (_transactionLock)
            {
                Guard.Against.NegativeOrZero(amount.Amount, nameof(amount.Amount));

                var credit = Credit.Create(Id, amount);
                _credits.Add(credit);
                return credit;
            }
        }

        public Debit Withdraw(Money amount)
        {
            lock (_transactionLock)
            {
                Guard.Against.NegativeOrZero(amount.Amount, nameof(amount.Amount));

                var balance = Availability();
                var withdrawBalance = balance.Subtract(amount);
                if (withdrawBalance.Negative())
                {
                    throw new InvalidOperationException("Insufficient funds");
                }

                var debit = Debit.Create(Id, amount);
                _debits.Add(debit);
                return debit;
            }
        }

        public Transfer TransferTo(Account toAccount, Money amount)
        {
            lock (_transactionLock)
            {
                Guard.Against.Null(toAccount, nameof(toAccount));

                Guard.Against.NegativeOrZero(amount.Amount, nameof(amount.Amount));

                if (toAccount.AccountNumber == AccountNumber)
                {
                    throw new InvalidOperationException("Transfer cannot be done the same from/to account.");
                }

                var balance = Availability();
                var withdrawBalance = balance.Subtract(amount);
                if (withdrawBalance.Negative())
                {
                    throw new InvalidOperationException("Insufficient funds");
                }

                var credit = toAccount.Deposit(amount);
                var debit = Debit.Create(Id, amount);
                _debits.Add(debit);

                var transfer = Transfer.Create(debit, credit);
                return transfer;
            }
        }

        public Money Availability()
        {
            var creditTotal = new Money(_credits.Sum(x => x.Amount.Amount));
            var debitTotal = new Money(_debits.Sum(x => x.Amount.Amount));
            return creditTotal.Subtract(debitTotal);
        }

        private Account() { }
    }
}
