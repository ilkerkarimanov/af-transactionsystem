using AF.TransactionSystem.Domain;

namespace AF.TransactionSystem.Infrastructure
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TransactionSystemDbContext _dbContext;

        public AccountRepository(TransactionSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Add(Account acc)
        {
            var account = _dbContext.Accounts.FirstOrDefault(x => x.AccountNumber == acc.AccountNumber);
            if (account is not null)
            {
                throw new ArgumentException("Account already exists.");
            }
            _dbContext.Accounts.Add(acc);
            return Task.CompletedTask;
        }

        public Task Add(Debit debit)
        {
            _dbContext.Debits.Add(debit);
            return Task.CompletedTask;
        }

        public Task Add(Credit credit)
        {
            _dbContext.Credits.Add(credit);
            return Task.CompletedTask;
        }

        public Task Add(Transfer transfer)
        {
            Add(transfer.Credit);
            Add(transfer.Debit);
            _dbContext.Transfers.Add(transfer);
            return Task.CompletedTask;
        }

        public Task<Account> Find(AccountNumber accountNumber)
        {
            var account = _dbContext.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
            if (account is null)
            {
                throw new ArgumentException("Account cannot be found.");
            }
            return Task.FromResult(account);
        }

        public Task SaveChanges()
        {
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
