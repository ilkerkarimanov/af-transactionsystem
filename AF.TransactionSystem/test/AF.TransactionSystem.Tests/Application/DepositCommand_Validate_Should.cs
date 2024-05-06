using AF.TransactionSystem.Application;
using FluentValidation;
using Xunit;

namespace AF.TransactionSystem.Tests.Application
{
    public class DepositCommand_Validate_Should
    {

        [Theory]
        [InlineData("rr1", "1")]
        public void Valid_When_InputParametersAreCorrect(string accountNumber, string amount)
        {
            var obj = new DepositCommand(accountNumber, amount);
            IValidator<DepositCommand> validator = new DepositCommandValidator();

            var result = validator.Validate(obj);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Theory]
        [InlineData("", "1")]
        [InlineData("", "1")]
        [InlineData("rr1", "")]
        [InlineData("rr1", "q")]
        [InlineData("rr1", "0")]
        [InlineData("rr1", "-1")]
        public void NotValid_When_InputParametersAreIncorrect(string accountNumber, string amount)
        {
            var obj = new DepositCommand(accountNumber, amount);
            IValidator<DepositCommand> validator = new DepositCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }
}
