using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace AF.TransactionSystem.Domain
{
    public readonly record struct AccountId
    {
        public Guid Value { get; init; }
        public AccountId() { Value = Guid.NewGuid(); }
    }

    public class Account : EntityBase<AccountId>
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
                try
                {
                    Guard.Against.NegativeOrZero(amount.Amount, nameof(amount.Amount));

                    var credit = Credit.Create(Id, amount);
                    _credits.Add(credit);
                    return credit;
                }
                catch (Exception ex)
                {
                    throw new CreditOperationException(AccountNumber.Value, amount.Amount, string.Empty, ex);
                }
            }
        }

        public Debit Withdraw(Money amount)
        {
            lock (_transactionLock)
            {
                try
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
                catch (Exception ex)
                {
                    throw new DebitOperationException(AccountNumber.Value, amount.Amount, string.Empty, ex);
                }
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
