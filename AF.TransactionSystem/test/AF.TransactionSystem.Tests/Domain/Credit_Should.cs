using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Domain
{
    public class Credit_Should
    {
        [Fact]
        public void ReturnsObject_When_AmountIsPositive()
        {
            var obj = Credit.Create(
                new AccountId(),
                new Money(1));
            Assert.IsType<Credit>(obj);
        }

        [Fact]
        public void ThrowsException_When_AmountIsZero()
        {
            Assert.Throws<ArgumentException>(() => Credit.Create(
                new AccountId(),
                new Money(0)));
        }

        [Fact]
        public void ThrowsException_When_AmountIsNegative()
        {
            Assert.Throws<ArgumentException>(() => Credit.Create(
                new AccountId(),
                new Money(-1)));
        }
    }
}
