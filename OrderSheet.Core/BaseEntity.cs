namespace OrderSheet.Core
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; }

        protected BaseEntity()
        {

        }

        protected BaseEntity(TKey key)
        {
            Id = key;
        }
    }
}
