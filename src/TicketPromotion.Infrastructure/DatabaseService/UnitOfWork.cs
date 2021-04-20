using TicketTypePromotion.Application.Common.Interfaces;

namespace TicketTypePromotion.Infrastructure.DatabaseService
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPromotionRepository PromotionRepository { get; }
        public ITicketTypeRepository TicketTypeRepository { get;  }
        public IReservationRepository ReservationRepository { get; }

        public UnitOfWork(IPromotionRepository promotionRepository,
                          ITicketTypeRepository ticketTypeRepository,
                          IReservationRepository orderRepository)
        {
            PromotionRepository = promotionRepository;
            TicketTypeRepository = ticketTypeRepository;
            ReservationRepository = orderRepository;
        }

        public void Commit()
        {
           //commit
        }

        public void SaveChanges()
        {
            //save changes with Dbcontext
        }

        public void Rollback()
        {
            //rollback
        }

        public void Dispose()
        {
            //dispose
        }
    }
}
