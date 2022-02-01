
using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Products.Rules
{
    public class ProductCodeCannotBeNull : IBusinessRule
    {
        private readonly string _productCode;
        public ProductCodeCannotBeNull(string productCode)
        {
            _productCode = productCode;
        }

        public string Message => MessageConstants.NullProductCodeError;

        public bool IsBroken() => _productCode == null;
    }
}
