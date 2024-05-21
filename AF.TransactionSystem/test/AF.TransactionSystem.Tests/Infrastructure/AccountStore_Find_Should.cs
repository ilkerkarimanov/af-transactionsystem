using AF.TransactionSystem.Infrastructure;
using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Infrastructure
{
    /*
    public class AccountStore_Should
    {
        [Fact]
        public async Task Completes_When_FindExistingAccount()
        {
            IAccountRepository store = new AccountStore();
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );

            await store.Add(account);

            var result = await store.Find(account.AccountNumber);
            Assert.Equal(account.Id, result.Id);
            Assert.Equal(account.AccountNumber, result.AccountNumber);
            Assert.Equal(account.Name, result.Name);
        }

        [Fact]
        public async Task Throws_When_FindsNonExistingAccount()
        {
            IAccountRepository store = new AccountStore();
            var accountNumber = AccountNumber.Create("rr1");

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await store.Find(accountNumber));
        }
    }
    */
}
