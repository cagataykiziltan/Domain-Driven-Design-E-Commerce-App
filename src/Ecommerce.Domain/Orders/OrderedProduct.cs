
using System;
using Ecommence.Domain.Products.Rules;
using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Orders
{
    public class OrderedProduct : EntityObject
    {
        public string ProductCode { get; private set; }
        public double Price { get; private set; }
        public string AppliedCampaign { get; private set; }

        public static OrderedProduct Create(string productCode, double price, string appliedCampaign)
        {
            CheckRule(new ProductCodeCannotBeNull(productCode));
            CheckRule(new PromotedPriceCannotBeNegative(price));
        
            var product = new OrderedProduct
            {
                Id = Guid.NewGuid(),
                ProductCode = productCode,
                Price = price,
                AppliedCampaign = appliedCampaign

            };

            return product;
        }

    }
}
