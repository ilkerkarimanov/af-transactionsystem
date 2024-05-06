using AF.TransactionSystem.Application;
using FluentValidation;
using Xunit;

namespace AF.TransactionSystem.Tests.Application
{
    public class TransferCommand_Validate_Should
    {

        [Theory]
        [InlineData("rr1","rr2", "1")]
        public void Valid_When_InputParametersAreCorrect(string fromAccountNumber, string toAccountNumber, string amount)
        {
            var obj = new TransferCommand(fromAccountNumber, toAccountNumber, amount);
            IValidator<TransferCommand> validator = new TransferCommandValidator();

            var result = validator.Validate(obj);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("", "rr2", "1")]
        [InlineData("rr1", "", "1")]
        [InlineData("rr1", "rr2", "")]
        [InlineData("rr1", "rr2", "q")]
        [InlineData("rr1", "rr2", "0")]
        [InlineData("rr1", "rr2", "-1")]
        public void NotValid_When_InputParametersAreIncorrect(string fromAccountNumber, string toAccountNumber, string amount)
        {
            var obj = new TransferCommand(fromAccountNumber, toAccountNumber, amount);
            IValidator<TransferCommand> validator = new TransferCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }
}
