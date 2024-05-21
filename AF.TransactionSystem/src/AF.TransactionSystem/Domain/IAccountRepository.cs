namespace AF.TransactionSystem.Domain
{
    public interface IAccountRepository
    {
        Task Add(Account acc);
        Task Add(Debit debit);
        Task Add(Credit credit);
        Task Add(Transfer transfer);
        Task<Account> Find(AccountNumber accountNumber);
        Task SaveChanges();
    }
}
