using TicketTypePromotion.Domain.Reservations.Rules;
using TicketTypePromotion.Domain.TicketTypes.Rules;
using System;
using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.Reservations
{
    public class Reservation : EntityObject
    {
        public ReservationedTicketType TicketType { get; private set; }
        public int Quantity { get; private set; }

        private Reservation() { }

        public static Reservation Create(string ticketCode, double price, string appliedPromotionName, int quantity)
        {
            CheckRule(new TicketTypeCodeCannotBeNull(ticketCode));
            CheckRule(new QuantityCannotBeZeroOrNegative(quantity));
            CheckRule(new PromotedPriceCannotBeNegative(price));
            
            var order = new Reservation
            {
                Id = Guid.NewGuid(),
                TicketType = ReservationedTicketType.Create( ticketCode,  price,  appliedPromotionName),
                Quantity = quantity
            };

        
            return order;
        }
    }
}
