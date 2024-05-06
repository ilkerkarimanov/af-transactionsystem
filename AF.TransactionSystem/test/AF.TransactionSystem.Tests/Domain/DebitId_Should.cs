using AF.TransactionSystem.Domain;
using Xunit;

namespace AF.TransactionSystem.Tests.Domain
{
    public class DebitId_Should
    {
        [Fact]
        public void ReturnsObject_When_Valid()
        {
            var obj = DebitId.Create(Guid.NewGuid());
            Assert.IsType<DebitId>(obj);
        }

        [Fact]
        public void ThrowsException_When_Empty()
        {
            Assert.Throws<ArgumentException>(() => DebitId.Create(Guid.Empty));
        }
    }
}
