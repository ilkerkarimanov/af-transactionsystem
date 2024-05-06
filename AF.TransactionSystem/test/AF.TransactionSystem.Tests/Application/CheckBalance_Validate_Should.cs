using AF.TransactionSystem.Application;
using FluentValidation;
using Xunit;

namespace AF.TransactionSystem.Tests.Application
{
    public class CheckBalance_Validate_Should
    {

        [Theory]
        [InlineData("rr_1")]
        [InlineData("rr1")]
        public void Valid_When_InputParametersAreCorrect(string accountNumber)
        {
            var obj = new CheckBalanceQuery(accountNumber);
            IValidator<CheckBalanceQuery> validator = new CheckBalanceQueryValidator();

            var result = validator.Validate(obj);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NotValid_When_InputParametersAreIncorrect(string accountNumber)
        {
            var obj = new CheckBalanceQuery(accountNumber);
            IValidator<CheckBalanceQuery> validator = new CheckBalanceQueryValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }
}
