using Ecommence.Domain.Orders;
using System.Collections.Generic;
using System.Linq;
using Ecommence.Application.Common.Interfaces;

namespace Ecommence.Infrastructure.DatabaseService
{
   public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders;
        public OrderRepository() 
        {
            _orders = new List<Order>();
        }
        public void Create(Order order) => _orders.Add(order);

        public List<Order> GetOrdersByCampaignCode(string campaignCode) => _orders.Where(x => x.Product.AppliedCampaign == campaignCode).ToList();
    }
}
