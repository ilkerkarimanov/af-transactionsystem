using AF.TransactionSystem.Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AF.TransactionSystem.Application
{
    public record DepositCommand(
    string AccountNumber,
    string Amount) : IRequest
    { }

    public class DepositCommandValidator : AbstractValidator<DepositCommand>
    {
        public DepositCommandValidator()
        {
            RuleFor(x => x.AccountNumber).NotEmpty();
            RuleFor(x => x.Amount).Must(x => decimal.TryParse(x, out var number) && number > 0);
        }
    }

    public class DepositCommandHandler : IRequestHandler<DepositCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IValidator<DepositCommand> _validator;
        private readonly ILogger<DepositCommandHandler> _logger;

        public DepositCommandHandler(
            IAccountRepository accountRepository,
            IValidator<DepositCommand> validator,
            ILogger<DepositCommandHandler> logger)
        {
            _accountRepository = accountRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var accountNumber = AccountNumber.Create(request.AccountNumber);
            var account = await _accountRepository.Find(accountNumber);

            if(account is null)
            {
                throw new ArgumentNullException($"Account cannot be found with account number: {request.AccountNumber}");
            }

            var depositAmount = new Money(decimal.Parse(request.Amount));
            var credit = account.Deposit(depositAmount);

            _logger.LogInformation($"Deposit has been done. AccountNumber {credit.AccountId.Value}, Amount: {credit.Amount.Amount}");
        }
    }
}
