using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Domain
{
    public class Account_Withdraw_Should
    {
        [Fact]
        public void BalanceCorrect_When_AmountIsPositive()
        {
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot","robotkin")
                );
            account.Deposit(new Money(1));
            account.Withdraw(new Money(1));
            Assert.Equal(new Money(0), account.Availability());
        }

        [Fact]
        public void ThrowsException_When_AmountIsZero()
        {
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );
            account.Deposit(new Money(1));
            Assert.Throws<DebitOperationException>(() => account.Withdraw(new Money(0)));
        }

        [Fact]
        public void ThrowsException_When_AmountIsNegative()
        {
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );
            account.Deposit(new Money(1));
            Assert.Throws<DebitOperationException>(() => account.Withdraw(new Money(0)));
        }

        [Fact]
        public void ThrowsException_When_InsufficientFunds()
        {
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );
            account.Deposit(new Money(1));
            Assert.Throws<DebitOperationException>(() => account.Withdraw(new Money(2)));
        }
    }
}
