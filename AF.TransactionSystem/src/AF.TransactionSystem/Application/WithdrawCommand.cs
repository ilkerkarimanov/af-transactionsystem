using AF.TransactionSystem.Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AF.TransactionSystem.Application
{
    public record WithdrawCommand(
    string AccountNumber,
    string Amount) : IRequest
    { }

    public class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
    {
        public WithdrawCommandValidator()
        {
            RuleFor(x => x.AccountNumber).NotEmpty();
            RuleFor(x => x.Amount).Must(x => decimal.TryParse(x, out var number) && number > 0)
                .WithMessage("Amount must be valid positive number."); ;
        }
    }

    public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IValidator<WithdrawCommand> _validator;
        private readonly ILogger<WithdrawCommandHandler> _logger;

        public WithdrawCommandHandler(
            IAccountRepository accountRepository,
            IValidator<WithdrawCommand> validator,
            ILogger<WithdrawCommandHandler> logger)
        {
            _accountRepository = accountRepository;
            _validator = validator;
            _logger = logger;
        }

        public Task Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var accountNumber = AccountNumber.Create(request.AccountNumber);
            var account = _accountRepository.Find(accountNumber).Result;

            if(account is null)
            {
                throw new ArgumentNullException($"Account cannot be found with account number: {request.AccountNumber}");
            }

            var amount = new Money(decimal.Parse(request.Amount));
            var debit = account.Withdraw(amount);
            _accountRepository.Add(debit);
            _accountRepository.SaveChanges();

            _logger.LogInformation($"Debit has been done. AccountNumber {debit.AccountId.Value}, Amount: {debit.Amount.Amount}");

            return Task.CompletedTask;
        }
    }
}
