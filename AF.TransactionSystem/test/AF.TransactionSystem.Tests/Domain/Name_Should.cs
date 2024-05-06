using Xunit;
using AF.TransactionSystem.Domain;

namespace AF.TransactionSystem.Tests.Domain
{
    public class Name_Should
    {
        [Fact]
        public void ReturnsObject_When_FirstAndLastNameNonEmpty()
        {
            var name = Name.Create("Robot", "Robotkin");
            Assert.IsType<Name>(name);
        }

        [Fact]
        public void ThrowsException_When_FirstNameNull()
        {
            Assert.Throws<ArgumentNullException>(() => Name.Create(null, "Robotkin"));
        }


        [Fact]
        public void ThrowsException_When_FirstNameEmpty()
        {
            Assert.Throws<ArgumentException>(() => Name.Create("", "Robotkin"));
        }


        [Fact]
        public void ThrowsException_When_LastNameNull()
        {
            Assert.Throws<ArgumentNullException>(() => Name.Create("Robot", null));
        }


        [Fact]
        public void ThrowsException_When_LastNameEmpty()
        {
            Assert.Throws<ArgumentException>(() => Name.Create("Robot", ""));
        }
    }
}
