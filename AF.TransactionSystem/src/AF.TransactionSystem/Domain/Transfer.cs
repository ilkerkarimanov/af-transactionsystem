using Ardalis.SharedKernel;

namespace AF.TransactionSystem.Domain
{
    public readonly record struct TransferId
    {
        public Guid Value { get; init; }
        public TransferId() { Value = Guid.NewGuid(); }
    }

    public class Transfer : EntityBase<TransferId>
    { 
        public Credit Credit { get; init; }
        public CreditId CreditId { get; private set; }
        public Debit Debit { get; init; }
        public DebitId DebitId { get; private set; }
        public DateTime CreatedDate { get; init; }

        private Transfer() { }

        public static Transfer Create(
            Debit debit,
            Credit credit
            )
        {
            return new Transfer()
            {
                Id = new TransferId(),
                Debit = debit,
                DebitId = debit.Id,
                Credit = credit,
                CreditId = credit.Id,
                CreatedDate = DateTime.UtcNow
            };
        }
    }

    public class TransferOperationException : Exception
    {
        public TransferOperationException(string message) : base(message) { }
        public TransferOperationException(Exception ex) : base(ex.Message, ex) { }
    }
}
