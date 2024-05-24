using AF.TransactionSystem.Infrastructure;
using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Infrastructure
{

    public class AccountStore_Find_Should : EfCoreTestBase
    {
        [Fact]
        public void Completes_When_FindExistingAccount()
        {
            IAccountRepository store = new AccountRepository(_dbContext);
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );

            store.Add(account);
            store.SaveChanges();

            Account result = store.Find(account.AccountNumber).Result;

            Assert.Equal(1, _dbContext.Accounts.Count());
            Assert.Equal(account.Id, result.Id);
            Assert.Equal(account.AccountNumber, result.AccountNumber);
            Assert.Equal(account.Name, result.Name);
        }

        [Fact]
        public void Throws_When_FindsNonExistingAccount()
        {
            IAccountRepository store = new AccountRepository(_dbContext);
            var accountNumber = AccountNumber.Create("rr1");

            Assert.Throws<ArgumentException>(() => store.Find(accountNumber).RunSynchronously());
        }
    }
}
