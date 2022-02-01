using System;

namespace Ecommence.Application.Common.Interfaces
{
    public interface IUnitOfWork :  IDisposable
    {
       ICampaignRepository CampaignRepository { get; }
       IProductRepository ProductRepository { get; }
       IOrderRepository OrderRepository { get; }
       void Commit();
       void Rollback();
       void SaveChanges();
    }
}
