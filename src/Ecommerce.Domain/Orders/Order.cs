using Ecommence.Domain.Orders.Rules;
using Ecommence.Domain.Products.Rules;
using System;
using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Orders
{
    public class Order : EntityObject<Guid>
    {
        public OrderedProduct Product { get; private set; }
        public int Quantity { get; private set; }

        private Order() { }

        public static Order Create(Guid id, string productCode, double price, string appliedCampaignName, int quantity)
        {
            CheckRule(new ProductCodeCannotBeNull(productCode));
            CheckRule(new QuantityCannotBeZeroOrNegative(quantity));
            CheckRule(new PromotedPriceCannotBeNegative(price));
            
            var order = new Order
            {
                Id = id,
                Product = OrderedProduct.Create( productCode,  price,  appliedCampaignName),
                Quantity = quantity
            };

        
            return order;
        }
    }
}
