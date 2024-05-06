using AF.TransactionSystem.Domain;
using FluentValidation;
using MediatR;

namespace AF.TransactionSystem.Application
{
    public record CheckBalanceQuery(string AccountNumber) : IRequest<CheckBalanceQueryResult> { }

    public record CheckBalanceQueryResult(decimal Amount);

    public class CheckBalanceQueryValidator : AbstractValidator<CheckBalanceQuery>
    {
        public CheckBalanceQueryValidator()
        {
            RuleFor(x => x.AccountNumber).NotEmpty();
        }
    }

    public class CheckBalanceQueryHandler : IRequestHandler<CheckBalanceQuery, CheckBalanceQueryResult>
    {
        private readonly IAccountRepository _accountRepository;

        public CheckBalanceQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CheckBalanceQueryResult> Handle(CheckBalanceQuery request, CancellationToken cancellationToken)
        {
            var accountNumber = AccountNumber.Create(request.AccountNumber);
            var account = await _accountRepository.Find(accountNumber);

            if (account is null)
            {
                throw new ArgumentNullException($"Account cannot be found with account number: {request.AccountNumber}");
            }

            return new CheckBalanceQueryResult(account.Availability().Amount);
        }
    }
}
