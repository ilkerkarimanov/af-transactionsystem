using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Domain
{
    public class Debit_Should
    {
        [Fact]
        public void ReturnsObject_When_AmountIsPositive()
        {
            var obj = Debit.Create(
                AccountId.Create(Guid.NewGuid()),
                new Money(1));
            Assert.IsType<Debit>(obj);
        }

        [Fact]
        public void ThrowsException_When_AmountIsZero()
        {
            Assert.Throws<ArgumentException>(() => Debit.Create(
                AccountId.Create(Guid.NewGuid()),
                new Money(0)));
        }

        [Fact]
        public void ThrowsException_When_AmountIsNegative()
        {
            Assert.Throws<ArgumentException>(() => Debit.Create(
                AccountId.Create(Guid.NewGuid()),
                new Money(-1)));
        }
    }
}
