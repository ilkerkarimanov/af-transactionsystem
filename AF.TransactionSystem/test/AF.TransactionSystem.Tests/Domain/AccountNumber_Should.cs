using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Domain
{
    public class AccountNumber_Should
    {
        [Fact]
        public void ReturnsObject_When_NonEmptyOrNull()
        {
            var obj = AccountNumber.Create("rr1");
            Assert.IsType<AccountNumber>(obj);
        }

        [Fact]
        public void ThrowsException_When_Null()
        {
            Assert.Throws<ArgumentNullException>(() => AccountNumber.Create(null));
        }


        [Fact]
        public void ThrowsException_When_Empty()
        {
            Assert.Throws<ArgumentException>(() => AccountNumber.Create(""));
        }
    }
}
