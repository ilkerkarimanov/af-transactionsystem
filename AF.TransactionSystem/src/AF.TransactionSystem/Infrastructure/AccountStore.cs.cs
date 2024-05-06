using AF.TransactionSystem.Domain;
using System.Collections.Concurrent;

namespace AF.TransactionSystem.Infrastructure
{
    public class AccountStore : IAccountRepository
    {
        public readonly ConcurrentDictionary<AccountNumber, Account> accounts = new ConcurrentDictionary<AccountNumber, Account>();

        public Task Add(Account acc)
        {
            if (accounts.TryAdd(acc.AccountNumber, acc))
            {
                return Task.CompletedTask;
            }
            else
            {
                throw new InvalidOperationException($"Account already exists with account number {acc.AccountNumber.Value}");
            }
        }

        public Task<Account> Find(AccountNumber accountNumber)
        {
            if (accounts.TryGetValue(accountNumber, out Account acc))
            {
                return Task.FromResult(acc);
            }
            else
            {
                throw new InvalidOperationException($"Account doesn't exists with account number {accountNumber.Value}");
            }
        }
    }
}
