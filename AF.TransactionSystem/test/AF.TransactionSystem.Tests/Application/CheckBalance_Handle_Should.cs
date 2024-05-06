using AF.TransactionSystem.Domain;
using AF.TransactionSystem.Application;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;

namespace AF.TransactionSystem.Tests.Application
{
    public class CheckBalance_Handle_Should
    {
        [Fact]
        public async Task Completes_When_AccountExists()
        {
            var query = new CheckBalanceQuery("rr1");
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );
            account.Deposit(new Money(13));
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(x => x.Find(It.IsAny<AccountNumber>())).ReturnsAsync(account);

            var handler = new CheckBalanceQueryHandler(
                mockRepo.Object
                );

            var result = await handler.Handle(query, new CancellationToken());
            Assert.Equal(13, result.Amount);
        }

        [Fact]
        public async Task Throws_When_AccountDoesntExists()
        {
            var query = new CheckBalanceQuery("rr1");
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(x => x.Find(It.IsAny<AccountNumber>())).ReturnsAsync(default(Account));
            var handler = new CheckBalanceQueryHandler(
                mockRepo.Object
                );

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(query, new CancellationToken()));

        }
    }
}
