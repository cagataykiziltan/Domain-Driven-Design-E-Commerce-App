using System;

namespace Ecommence.Domain.SeedWork
{
    public abstract class EntityObject<TId> : IEquatable<EntityObject<TId>>
    {
        private TId id;

        public TId Id
        {
            get { return id; }
            set { id = value; }
        }

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
      
        public bool Equals(EntityObject<TId> other)
        {
            var entity = other;
            if (entity != null)
            {
                return Equals(entity);
            }
            return base.Equals(other);
        }
    }
}
