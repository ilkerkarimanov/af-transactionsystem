using AF.TransactionSystem.Application;
using FluentValidation;
using Xunit;

namespace AF.TransactionSystem.Tests.Application
{
    public class OpenAccountCommand_Validate_Should
    {

        [Fact]
        public void Valid_When_InputParametersAreCorrect()
        {
            var obj = new OpenAccountCommand("rr1", "robot", "robotkin", "1");
            IValidator<OpenAccountCommand> validator = new OpenAccountCommandValidator();

            var result = validator.Validate(obj);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void NotValid_When_AccountNumberIsNull()
        {
            var obj = new OpenAccountCommand(null, "robot", "robotkin", "1");
            IValidator<OpenAccountCommand> validator = new OpenAccountCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }


        [Fact]
        public void NotValid_When_AccountNumberIsEmpty()
        {
            var obj = new OpenAccountCommand("", "robot", "robotkin", "1");
            IValidator<OpenAccountCommand> validator = new OpenAccountCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void NotValid_When_FirstNameIsNull()
        {
            var obj = new OpenAccountCommand("rr1", null, "robotkin", "1");
            IValidator<OpenAccountCommand> validator = new OpenAccountCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void NotValid_When_FirstNameIsEmpty()
        {
            var obj = new OpenAccountCommand("rr1", "", "robotkin", "1");
            IValidator<OpenAccountCommand> validator = new OpenAccountCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void NotValid_When_LastNameIsNull()
        {
            var obj = new OpenAccountCommand("rr1", "robot", null, "1");
            IValidator<OpenAccountCommand> validator = new OpenAccountCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void NotValid_When_LastNameIsEmpty()
        {
            var obj = new OpenAccountCommand("rr1", "robot", "", "1");
            IValidator<OpenAccountCommand> validator = new OpenAccountCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void NotValid_When_AmountIsNull()
        {
            var obj = new OpenAccountCommand("rr1", "robot", "robotkin", null);
            IValidator<OpenAccountCommand> validator = new OpenAccountCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void NotValid_When_AmountIsEmpty()
        {
            var obj = new OpenAccountCommand("rr1", "robot", "robotkin", "");
            IValidator<OpenAccountCommand> validator = new OpenAccountCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void NotValid_When_AmountIsNonNumber()
        {
            var obj = new OpenAccountCommand("rr1", "robot", "robotkin", "q");
            IValidator<OpenAccountCommand> validator = new OpenAccountCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void NotValid_When_AmountIsZero()
        {
            var obj = new OpenAccountCommand("rr1", "robot", "robotkin", "0");
            IValidator<OpenAccountCommand> validator = new OpenAccountCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void NotValid_When_AmountIsNegative()
        {
            var obj = new OpenAccountCommand("rr1", "robot", "robotkin", "0");
            IValidator<OpenAccountCommand> validator = new OpenAccountCommandValidator();

            var result = validator.Validate(obj);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }
}
