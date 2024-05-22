using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Domain
{
    public class AccountId_Should
    {
        [Fact]
        public void ReturnsObject_When_Valid()
        {
            var obj = new AccountId(Guid.NewGuid());
            Assert.IsType<AccountId>(obj);
        }

        [Fact]
        public void ThrowsException_When_Empty()
        {
            Assert.Throws<ArgumentException>(() => new AccountId(Guid.Empty));
        }
    }
}
