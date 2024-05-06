namespace AF.TransactionSystem.Domain
{
    public abstract class Entity<EntityId>
    {
        EntityId _Id;
        public virtual EntityId Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }
    }
}
