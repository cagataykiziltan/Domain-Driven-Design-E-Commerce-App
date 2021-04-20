using System;

namespace TicketTypePromotion.Application.Common.Interfaces
{
    public interface IUnitOfWork :  IDisposable
    {
       IPromotionRepository PromotionRepository { get; }
       ITicketTypeRepository TicketTypeRepository { get; }
       IReservationRepository ReservationRepository { get; }
       void Commit();
       void Rollback();
       void SaveChanges();
    }
}
