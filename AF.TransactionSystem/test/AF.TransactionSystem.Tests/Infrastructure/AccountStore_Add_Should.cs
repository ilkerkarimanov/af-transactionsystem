using AF.TransactionSystem.Infrastructure;
using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Infrastructure
{
    /*
    public class AccountStore_Add_Should
    {
        [Fact]
        public void Completes_When_OnAddAccountOnce()
        {
            IAccountRepository store = new AccountStore();
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );

            store.Add(account);
        }

        [Fact]
        public async Task Throws_When_OnAddAccountTwoTimes()
        {
            IAccountRepository store = new AccountStore();
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );
            await store.Add(account);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await store.Add(account));
        }
    }
    */
}
