
using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.TicketTypes.Rules
{
    public class TicketTypeCodeCannotBeNull : IBusinessRule
    {
        private readonly string _ticketCode;
        public TicketTypeCodeCannotBeNull(string ticketCode)
        {
            _ticketCode = ticketCode;
        }

        public string Message => MessageConstants.NullTicketTypeCodeError;

        public bool IsBroken() => _ticketCode == null;
    }
}
