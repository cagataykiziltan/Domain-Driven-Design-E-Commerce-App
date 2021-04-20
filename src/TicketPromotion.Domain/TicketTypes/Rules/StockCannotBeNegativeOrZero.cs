using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.TicketTypes.Rules
{
    class StockCannotBeNegativeOrZero : IBusinessRule
    {
        private readonly double _stock;
        public StockCannotBeNegativeOrZero(double stock)
        {
            _stock = stock;
        }

        public string Message => MessageConstants.NegativeOrZeroStockError;

        public bool IsBroken() => _stock <= 0;
    }
  
}
