using Ardalis.GuardClauses;

namespace AF.TransactionSystem.Domain
{
    public interface ITransferService
    {
        Transfer Transfer(Account fromAccount, Account toAccount, Money amount);
    }

    public class TransferService : ITransferService
    {
        public Transfer Transfer(Account fromAccount, Account toAccount, Money amount)
        {
            Guard.Against.Null(fromAccount, nameof(fromAccount));

            Guard.Against.Null(toAccount, nameof(toAccount));

            Guard.Against.NegativeOrZero(amount.Amount, nameof(amount.Amount));

            if (toAccount.AccountNumber == fromAccount.AccountNumber)
            {
                throw new TransferOperationException("Transfer cannot be done the same from/to accounts.");
            }

            try
            {
                var debit = fromAccount.Withdraw(amount);
                var credit = toAccount.Deposit(amount);
                var transfer = Domain.Transfer.Create(debit, credit);
                return transfer;
            }
            catch (CreditOperationException creditEx)
            {
                throw new TransferOperationException($"Transfer cannot be processed, because credit failed. \n Amount: {amount.Amount} \n Debit: {toAccount.AccountNumber.Value}  \n Credit: {fromAccount.AccountNumber.Value}", creditEx);

            }
            catch (DebitOperationException debitEx)
            {
                fromAccount.Deposit(amount);
                throw new TransferOperationException($"Transfer cannot be processed, because debit failed. \n Amount: {amount.Amount} \n Debit: {toAccount.AccountNumber.Value}  \n Credit: {fromAccount.AccountNumber.Value}." +
                    $" \n Reverse credit was issued to account {fromAccount.AccountNumber.Value}.", debitEx);
            }
        }
    }
}
