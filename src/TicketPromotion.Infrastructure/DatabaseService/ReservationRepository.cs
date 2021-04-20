using TicketTypePromotion.Domain.Reservations;
using System.Collections.Generic;
using System.Linq;
using TicketTypePromotion.Application.Common.Interfaces;

namespace TicketTypePromotion.Infrastructure.DatabaseService
{
   public class ReservationRepository : IReservationRepository
    {
        private readonly List<Reservation> _reservations;
        public ReservationRepository() 
        {
            _reservations = new List<Reservation>();
        }
        public void Create(Reservation order) => _reservations.Add(order);

        public List<Reservation> GetReservationsByPromotionCode(string promotionCode) => _reservations.Where(x => x.TicketType.AppliedPromotion == promotionCode).ToList();
    }
}
