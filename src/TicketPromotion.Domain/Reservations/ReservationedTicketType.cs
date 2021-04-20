
using System;
using TicketTypePromotion.Domain.TicketTypes.Rules;
using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.Reservations
{
    public class ReservationedTicketType : EntityObject
    {
        public string TicketTypeCode { get; private set; }
        public double Price { get; private set; }
        public string AppliedPromotion { get; private set; }

        public static ReservationedTicketType Create(string ticketCode, double price, string appliedPromotion)
        {
            CheckRule(new TicketTypeCodeCannotBeNull(ticketCode));
            CheckRule(new PromotedPriceCannotBeNegative(price));
        
            var ticketType = new ReservationedTicketType
            {
                Id = Guid.NewGuid(),
                TicketTypeCode = ticketCode,
                Price = price,
                AppliedPromotion = appliedPromotion

            };

            return ticketType;
        }

    }
}
