using Ecommence.Application.Common.Interfaces;

namespace Ecommence.Infrastructure.DatabaseService
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICampaignRepository CampaignRepository { get; }
        public IProductRepository ProductRepository { get;  }
        public IOrderRepository OrderRepository { get; }

        public UnitOfWork(ICampaignRepository campaignRepository,
                          IProductRepository productRepository,
                          IOrderRepository orderRepository)
        {
            CampaignRepository = campaignRepository;
            ProductRepository = productRepository;
            OrderRepository = orderRepository;
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
