using System;
using System.Collections.Generic;

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

        public override bool Equals(object obj)
        {
            var entity = obj as BaseEntity<TKey>;

            return entity != null &&
                   GetType() == entity.GetType() &&
                   EqualityComparer<TKey>.Default.Equals(Id, entity.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), Id);
        }

        public static bool operator ==(BaseEntity<TKey> entity1, BaseEntity<TKey> entity2)
        {
            return EqualityComparer<BaseEntity<TKey>>.Default.Equals(entity1, entity2);
        }

        public static bool operator !=(BaseEntity<TKey> entity1, BaseEntity<TKey> entity2)
        {
            return !(entity1 == entity2);
        }
    }
}
