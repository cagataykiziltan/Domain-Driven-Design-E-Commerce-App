using TicketTypePromotion.Application.TicketTypeServices;

namespace TicketTypePromotion.Application.Common.Interfaces
{
    public interface ITicketTypeService
    {
        TicketTypeDto CreateTicketType(TicketTypeDto ticketType);

        TicketTypeDto GetTicketType(string ticketCode);

    }
}
