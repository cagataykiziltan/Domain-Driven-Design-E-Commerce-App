using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Orders.Rules
{
 
    public class QuantityCannotBeZeroOrNegative : IBusinessRule
    {
        private readonly double _quantity;
        public QuantityCannotBeZeroOrNegative(double quantity)
        {
            _quantity = quantity;
        }

        public string Message => MessageConstants.QuantityValueLimitError;

        public bool IsBroken() => _quantity <= 0;
    }
}
