﻿using AF.TransactionSystem.Domain;
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
            RuleFor(x => x.Amount).Must(x => decimal.TryParse(x, out var number) && number > 0);
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

        public async Task Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var accountNumber = AccountNumber.Create(request.AccountNumber);
            var account = await _accountRepository.Find(accountNumber);

            if(account is null)
            {
                throw new ArgumentNullException($"Account cannot be found with account number: {request.AccountNumber}");
            }

            var amount = new Money(decimal.Parse(request.Amount));
            var debit = account.Withdraw(amount);

            _logger.LogInformation($"Debit has been done. AccountNumber {debit.AccountNumber.Value}, Amount: {debit.Amount.Amount}");
        }
    }
}
