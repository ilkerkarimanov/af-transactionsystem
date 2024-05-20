using AF.TransactionSystem.Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AF.TransactionSystem.Application
{
    public record TransferCommand(
        string FromAccountNumber,
        string ToAccountNumber,
        string Amount) : IRequest
    {
    }

    public class TransferCommandValidator : AbstractValidator<TransferCommand>
    {
        public TransferCommandValidator()
        {
            RuleFor(x => x.FromAccountNumber).NotEmpty();
            RuleFor(x => x.ToAccountNumber).NotEmpty();
            RuleFor(x => x.Amount).Must(x => decimal.TryParse(x, out var number) && number > 0)
                .WithMessage("Amount must be valid positive number."); ;
        }
    }

    public class TransferCommandHandler : IRequestHandler<TransferCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IValidator<TransferCommand> _validator;
        private readonly ILogger<TransferCommandHandler> _logger;
        private readonly ITransferService _transferService;

        public TransferCommandHandler(IAccountRepository accountRepository,
            IValidator<TransferCommand> validator,
            ILogger<TransferCommandHandler> logger,
            ITransferService transferService)
        {
            _accountRepository = accountRepository;
            _validator = validator;
            _logger = logger;
            _transferService = transferService;
        }

        public async Task Handle(TransferCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var fromAccountNumber = AccountNumber.Create(request.FromAccountNumber);
            var fromAccount = await _accountRepository.Find(fromAccountNumber);

            if (fromAccount is null)
            {
                throw new ArgumentNullException($"Account cannot be found with account number: {request.FromAccountNumber}");
            }

            var toAccountNumber = AccountNumber.Create(request.ToAccountNumber);
            var toAccount = await _accountRepository.Find(toAccountNumber);

            if (toAccount is null)
            {
                throw new ArgumentNullException($"Account cannot be found with account number: {request.ToAccountNumber}");
            }

            var amount = new Money(decimal.Parse(request.Amount));

            try
            {
                _transferService.Transfer(fromAccount, toAccount, amount);
                throw new TransferOperationException($"Transfer has been done. \n Amount: {amount.Amount} \n Debit: {fromAccount.AccountNumber.Value}  \n Credit: {toAccount.AccountNumber.Value}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transfer failed. Reason: \n {ex.Message}");
            }
        }
    }
}
