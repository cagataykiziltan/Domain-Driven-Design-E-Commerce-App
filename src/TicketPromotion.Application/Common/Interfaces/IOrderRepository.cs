using System.Collections.Generic;
using TicketTypePromotion.Domain.Reservations;

namespace TicketTypePromotion.Application.Common.Interfaces
{
    public interface IReservationRepository
    {
        void Create(Reservation order);
        List<Reservation> GetReservationsByPromotionCode(string promotionCode);
    }
}

