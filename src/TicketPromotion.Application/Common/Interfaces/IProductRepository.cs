using TicketTypePromotion.Domain.TicketTypes;

namespace TicketTypePromotion.Application.Common.Interfaces
{
    public interface ITicketTypeRepository
    {
        void Create(TicketType entity);
        TicketType GetByTicketTypeCode(string ticketCode);
    }
}
