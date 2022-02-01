
using Ecommence.Application.OrderServices;
using Ecommence.Domain.Products;
using System.Threading.Tasks;

namespace Ecommence.Application.Common.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrder(OrderDto order);
        Task<AppliedCampaignInfoDto> ApplyCampaignToPrice(Product productDomain, int quantity);
    }
}
