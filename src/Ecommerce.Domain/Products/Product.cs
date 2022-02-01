using Ecommence.Domain.Products.Rules;
using System;
using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Products
{
    public class Product : EntityObject<Guid>
    {
        public string ProductCode { get; private set; }
        public double Price { get; private set; }
        public double? PromotedPrice { get; private set; }
        public int Stock { get; private set; }

        private Product()
        { }

        public static Product Create(Guid id,string productCode, double price, int stock)
        {
            CheckRule(new ProductCodeCannotBeNull(productCode));
            CheckRule(new PriceCannotBeNegativeOrZero(price));
            CheckRule(new StockCannotBeNegativeOrZero(stock));

            var product = new Product
            {
                Id = id,
                ProductCode = productCode,
                Price = price,
                Stock = stock
            };

            return product;
        }
        public void SetPromotedPrice(double promotedPrice)
        {
            CheckRule(new PromotedPriceCannotBeNegative(promotedPrice));

            PromotedPrice = promotedPrice;
        }
        public void DecreaseStockQuantityByQuantity(int quantity) => Stock -= quantity;

     

    }
}

