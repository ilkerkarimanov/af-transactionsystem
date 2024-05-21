using AF.TransactionSystem.Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AF.TransactionSystem.Application
{
    public record OpenAccountCommand(
        string AccountNumber,
        string FirstName,
        string LastName,
        string OpeningBalance) : IRequest
    { }

    public class OpenAccountCommandValidator : AbstractValidator<OpenAccountCommand>
    {
        public OpenAccountCommandValidator()
        {
            RuleFor(x => x.AccountNumber).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.OpeningBalance).Must(x => decimal.TryParse(x, out var number) && number > 0)
                .WithMessage("OpeningBalance must be valid positive number."); ;
        }
    }

    public class OpenAccountCommandHandler : IRequestHandler<OpenAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IValidator<OpenAccountCommand> _validator;
        private readonly ILogger<OpenAccountCommandHandler> _logger;

        public OpenAccountCommandHandler(
            IAccountRepository accountRepository,
            IValidator<OpenAccountCommand> validator,
            ILogger<OpenAccountCommandHandler> logger)
        {
            _accountRepository = accountRepository; 
            _validator = validator;
            _logger = logger;
        }

        public Task Handle(OpenAccountCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.AccountNumber);
            _validator.ValidateAndThrow(request);

            var accountNumber = AccountNumber.Create(request.AccountNumber);

            var name = Name.Create(request.FirstName, request.LastName);
            var account = Account.Create(accountNumber, name);
            _accountRepository.Add(account);

            var openingAmount = new Money(decimal.Parse(request.OpeningBalance));
            var credit = account.Deposit(openingAmount);
            _accountRepository.Add(credit);
            _accountRepository.SaveChanges();

            _logger.LogInformation($"Open account has been done. AccountNumber {accountNumber.Value}");

            return Task.CompletedTask;
        }
    }
}
