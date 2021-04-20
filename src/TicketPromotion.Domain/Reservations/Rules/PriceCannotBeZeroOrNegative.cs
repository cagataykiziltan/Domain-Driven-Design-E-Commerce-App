using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.Reservations.Rules
{
    public class PriceCannotBeZeroOrNegative : IBusinessRule
    {
        private readonly double _price;
        public PriceCannotBeZeroOrNegative(double price)
        {
            _price = price;
        }

        public string Message => MessageConstants.NegativeOrZeroPriceError;

        public bool IsBroken() => _price <= 0;
    }
}
