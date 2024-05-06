using AF.TransactionSystem.Domain;
using AF.TransactionSystem.Application;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;

namespace AF.TransactionSystem.Tests.Application
{
    public class TransferCommand_Handle_Should
    {
        [Fact]
        public async Task Completes_When_AccountsExists()
        {
            var command = new TransferCommand("rr1", "jd1", "1");
            var fromAcc = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );
            fromAcc.Deposit(new Money(1));

            var toAcc = Account.Create(
                AccountNumber.Create("jd1"),
                Name.Create("john", "doe")
                );
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(x => x.Find(fromAcc.AccountNumber)).ReturnsAsync(fromAcc).Verifiable();
            mockRepo.Setup(x => x.Find(toAcc.AccountNumber)).ReturnsAsync(toAcc).Verifiable();

            var validator = new TransferCommandValidator();

            var mockLogger = new Mock<ILogger<TransferCommandHandler>>();

            var handler = new TransferCommandHandler(
                mockRepo.Object,
                validator,
                mockLogger.Object
                );

            await handler.Handle(command, new CancellationToken());

            mockRepo.Verify();
        }

        [Fact]
        public async Task Throws_When_FromAccountDoesntExists()
        {
            var command = new TransferCommand("rr1", "jd1", "1");
            var fromAcc = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );
            fromAcc.Deposit(new Money(1));

            var toAcc = Account.Create(
                AccountNumber.Create("jd1"),
                Name.Create("john", "doe")
                );
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(x => x.Find(fromAcc.AccountNumber)).ReturnsAsync(default(Account));
            mockRepo.Setup(x => x.Find(toAcc.AccountNumber)).ReturnsAsync(toAcc);

            var validator = new TransferCommandValidator();

            var mockLogger = new Mock<ILogger<TransferCommandHandler>>();

            var handler = new TransferCommandHandler(
                mockRepo.Object,
                validator,
                mockLogger.Object
                );

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(command, new CancellationToken()));
        }

        [Fact]
        public async Task Throws_When_ToAccountDoesntExists()
        {
            var command = new TransferCommand("rr1", "jd1", "1");
            var fromAcc = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );
            fromAcc.Deposit(new Money(1));

            var toAcc = Account.Create(
                AccountNumber.Create("jd1"),
                Name.Create("john", "doe")
                );
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(x => x.Find(fromAcc.AccountNumber)).ReturnsAsync(fromAcc);
            mockRepo.Setup(x => x.Find(toAcc.AccountNumber)).ReturnsAsync(default(Account));

            var validator = new TransferCommandValidator();

            var mockLogger = new Mock<ILogger<TransferCommandHandler>>();

            var handler = new TransferCommandHandler(
                mockRepo.Object,
                validator,
                mockLogger.Object
                );

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(command, new CancellationToken()));

        }
    }
}
