using AF.TransactionSystem.Domain;
using AF.TransactionSystem.Application;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;

namespace AF.TransactionSystem.Tests.Application
{
    public class DepositCommand_Handle_Should
    {
        [Fact]
        public async Task Completes_When_AccountExists()
        {
            var command = new DepositCommand("rr1", "1");
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(x => x.Find(It.IsAny<AccountNumber>())).ReturnsAsync(account).Verifiable();

            var validator = new DepositCommandValidator();

            var mockLogger = new Mock<ILogger<DepositCommandHandler>>();

            var handler = new DepositCommandHandler(
                mockRepo.Object,
                validator,
                mockLogger.Object
                );

            await handler.Handle(command, new CancellationToken());

            mockRepo.Verify();
        }

        [Fact]
        public async Task Throws_When_AccountDoesntExists()
        {
            var command = new DepositCommand("rr1", "1");
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(x => x.Find(It.IsAny<AccountNumber>())).ReturnsAsync(default(Account));

            var validator = new DepositCommandValidator();

            var mockLogger = new Mock<ILogger<DepositCommandHandler>>();

            var handler = new DepositCommandHandler(
                mockRepo.Object,
                validator,
                mockLogger.Object
                );

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(command, new CancellationToken()));

        }
    }
}
