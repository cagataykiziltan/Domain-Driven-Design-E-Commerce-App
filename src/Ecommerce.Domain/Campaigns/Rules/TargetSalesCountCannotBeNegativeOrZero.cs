using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Campaigns.Rules
{
    public class TargetSalesCountCannotBeNegativeOrZero : IBusinessRule
    {
        private readonly float _targetSalesCount;
        public TargetSalesCountCannotBeNegativeOrZero(float targetSalesCount)
        {
            _targetSalesCount = targetSalesCount;
        }

        public string Message => MessageConstants.TargetSalesCountError;

        public bool IsBroken() => _targetSalesCount <= 0;
    }
}
