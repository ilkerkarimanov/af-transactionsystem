using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Domain
{
    public class CreditId_Should
    {
        [Fact]
        public void ReturnsObject_When_Valid()
        {
            var obj = new CreditId(Guid.NewGuid());
            Assert.IsType<CreditId>(obj);
        }

        [Fact]
        public void ThrowsException_When_Empty()
        {
            Assert.Throws<ArgumentException>(() => new CreditId(Guid.Empty));
        }
    }
}
