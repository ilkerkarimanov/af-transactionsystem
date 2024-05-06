namespace AF.TransactionSystem.Domain
{
    public interface IAccountRepository
    {
        Task Add(Account acc);
        Task<Account> Find(AccountNumber accountNumber);
    }
}
