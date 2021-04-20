using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.Promotions.Rules
{
    public class PriceManipulationLimitMustBeBetween0And100 : IBusinessRule
    {
        private readonly float _priceManipulationLimit;
        public PriceManipulationLimitMustBeBetween0And100(float priceManipulationLimit)
        {
            _priceManipulationLimit = priceManipulationLimit;
        }

        public string Message => MessageConstants.PriceManipulationLimitError;

        public bool IsBroken() => _priceManipulationLimit <= 0 || _priceManipulationLimit>= 100;
    }
}
