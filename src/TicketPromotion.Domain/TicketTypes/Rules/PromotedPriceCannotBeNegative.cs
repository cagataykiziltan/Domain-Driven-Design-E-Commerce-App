using System;
using System.Collections.Generic;
using System.Text;
using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.TicketTypes.Rules
{
    class PromotedPriceCannotBeNegative : IBusinessRule
    {
        private readonly double _price;
        public PromotedPriceCannotBeNegative(double price)
        {
            _price = price;
        }

        public string Message => MessageConstants.PromotedPriceError;

        public bool IsBroken() => _price < 0;
    }
   
}
