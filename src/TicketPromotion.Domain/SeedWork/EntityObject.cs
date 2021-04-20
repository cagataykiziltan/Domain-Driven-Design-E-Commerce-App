using System;

namespace TicketTypePromotion.Domain.SeedWork
{
    public abstract class EntityObject
    {
        public Guid Id { get; set; }
        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
