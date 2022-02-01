using System.Collections.Generic;
using Ecommence.Domain.Orders;

namespace Ecommence.Application.Common.Interfaces
{
    public interface IOrderRepository
    {
        void Create(Order order);
        List<Order> GetOrdersByCampaignCode(string campaignCode);
    }
}

