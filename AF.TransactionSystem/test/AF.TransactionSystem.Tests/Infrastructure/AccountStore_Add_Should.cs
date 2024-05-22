using AF.TransactionSystem.Infrastructure;
using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Infrastructure
{
    public class AccountStore_Add_Should : EfCoreTestBase
    {
        [Fact]
        public void Completes_When_OnAddAccountOnce()
        {
            IAccountRepository store = new AccountRepository(_dbContext);
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );

            store.Add(account);
            store.SaveChanges();
        }

        [Fact]
        public void Throws_When_OnAddAccountTwoTimes()
        {
            IAccountRepository store = new AccountRepository(_dbContext);
            var account = Account.Create(
            AccountNumber.Create("rr1"),
            Name.Create("robot", "robotkin")
            );
            store.Add(account);
            store.SaveChanges();

            Assert.Throws<ArgumentException>(() => store.Add(account).RunSynchronously());
        }
    }
}
