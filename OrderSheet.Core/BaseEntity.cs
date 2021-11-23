namespace OrderSheet.Core
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; }

        public BaseEntity(TKey key)
        {
            Id = key;
        }
    }
}
