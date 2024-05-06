using AF.TransactionSystem.Domain;
using System.Security.Principal;
using Xunit;

namespace AF.TransactionSystem.Tests.Domain
{
    public class Account_Transfer_Should
    {
        [Fact]
        public void BalanceCorrect_When_AmountIsPositive()
        {
            var fromAcc = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot","robotkin")
                );

            fromAcc.Deposit(new Money(1));

            var toAcc = Account.Create(
                AccountNumber.Create("jd1"),
                Name.Create("john", "doe")
                );
            toAcc.Deposit(new Money(1));

            fromAcc.TransferTo(toAcc, new Money(1));

            Assert.Equal(new Money(0), fromAcc.Availability());
            Assert.Equal(new Money(2), toAcc.Availability());
        }

        [Fact]
        public void ThrowsException_When_AmountIsZero()
        {
            var fromAcc = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );

            var toAcc = Account.Create(
                AccountNumber.Create("jd1"),
                Name.Create("john", "doe")
                );

            Assert.Throws<ArgumentException>(() => fromAcc.TransferTo(toAcc, new Money(0)));
        }

        [Fact]
        public void ThrowsException_When_AmountIsNegative()
        {
            var fromAcc = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );

            var toAcc = Account.Create(
                AccountNumber.Create("jd1"),
                Name.Create("john", "doe")
                );

            Assert.Throws<ArgumentException>(() => fromAcc.TransferTo(toAcc, new Money(-1)));
        }

        [Fact]
        public void ThrowsException_When_InsufficientFunds()
        {
            var fromAcc = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );

            fromAcc.Deposit(new Money(1));

            var toAcc = Account.Create(
                AccountNumber.Create("jd1"),
                Name.Create("john", "doe")
                );
            toAcc.Deposit(new Money(1));

            Assert.Throws<InvalidOperationException>(() => fromAcc.TransferTo(toAcc, new Money(2)));
        }

        [Fact]
        public void ThrowsException_When_ToAccountIsNull()
        {
            var fromAcc = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );

            fromAcc.Deposit(new Money(1));

            Assert.Throws<ArgumentNullException>(() => fromAcc.TransferTo(null, new Money(1)));
        }

        [Fact]
        public void ThrowsException_When_FromAndToAccountsTheSame()
        {
            var fromAcc = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );

            fromAcc.Deposit(new Money(1));

            Assert.Throws<InvalidOperationException>(() => fromAcc.TransferTo(fromAcc, new Money(1)));
        }
    }
}
