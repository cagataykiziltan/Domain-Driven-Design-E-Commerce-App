using System;
using System.Collections.Generic;
using System.Text;
using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Products.Rules
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
