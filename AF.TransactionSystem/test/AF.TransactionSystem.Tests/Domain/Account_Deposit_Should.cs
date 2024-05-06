using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Domain
{
    public class Account_Deposit_Should
    {
        [Fact]
        public void BalanceCorrect_When_AmountIsPositive()
        {
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot","robotkin")
                );
            var amount = new Money(1);
            account.Deposit(new Money(1));
            Assert.Equal(amount, account.Availability());
        }

        [Fact]
        public void ThrowsException_When_AmountIsZero()
        {
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );
            var amount = new Money(0);
            Assert.Throws<ArgumentException>(() => account.Deposit(amount));
        }

        [Fact]
        public void ThrowsException_When_AmountIsNegative()
        {
            var account = Account.Create(
                AccountNumber.Create("rr1"),
                Name.Create("robot", "robotkin")
                );
            var amount = new Money(-1);
            Assert.Throws<ArgumentException>(() => account.Deposit(amount));
        }
    }
}
