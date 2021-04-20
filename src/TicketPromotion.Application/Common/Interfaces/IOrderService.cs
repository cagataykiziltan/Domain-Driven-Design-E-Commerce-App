
using TicketTypePromotion.Application.ReservationServices;
using TicketTypePromotion.Domain.TicketTypes;

namespace TicketTypePromotion.Application.Common.Interfaces
{
    public interface IReservationService
    {
        ReservationDto CreateReservation(ReservationDto order);
        AppliedPromotionInfoDto ApplyPromotionToPrice(TicketType ticketTypeDomain, int quantity);
    }
}
