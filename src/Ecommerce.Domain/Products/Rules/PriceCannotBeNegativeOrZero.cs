using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Products.Rules
{
    public class PriceCannotBeNegativeOrZero : IBusinessRule
    {
        private readonly double _price;
        public PriceCannotBeNegativeOrZero(double price)
        {
            _price = price;
        }

        public string Message => MessageConstants.NegativeOrZeroPriceError;

        public bool IsBroken() => _price <= 0;
    }
}
