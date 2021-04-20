using TicketTypePromotion.Domain.TicketTypes.Rules;
using System;
using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.TicketTypes
{
    public class TicketType : EntityObject
    {
        public string TicketTypeCode { get; private set; }
        public double Price { get; private set; }
        public double? PromotedPrice { get; private set; }
        public int Stock { get; private set; }

        private TicketType() { }

        public static TicketType Create(string ticketCode, double price, int stock)
        {
            CheckRule(new TicketTypeCodeCannotBeNull(ticketCode));
            CheckRule(new PriceCannotBeNegativeOrZero(price));
            CheckRule(new StockCannotBeNegativeOrZero(stock));

            var ticketType = new TicketType
            {
                Id = Guid.NewGuid(),
                TicketTypeCode = ticketCode,
                Price = price,
                Stock = stock
            };

            return ticketType;
        }
        public void SetPromotedPrice(double promotedPrice)
        {
            CheckRule(new PromotedPriceCannotBeNegative(promotedPrice));

            PromotedPrice = promotedPrice;
        }
        public void DecreaseStockQuantityByQuantity(int quantity) => Stock -= quantity;

     

    }
}

