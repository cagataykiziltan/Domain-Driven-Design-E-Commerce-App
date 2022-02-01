using System;
using System.Threading.Tasks;
using Ecommence.Application.Common.Aspects;
using Ecommence.Application.Common.Interfaces;
using Ecommence.Domain.Orders;
using Ecommence.Domain.Products;
using Ecommence.Domain.SeedWork;

namespace Ecommence.Application.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [LoggerAspect]
        [TransactionAspect]
        public async Task<OrderDto> CreateOrder(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new Exception(MessageConstants.NullParameterError);

            var product = _unitOfWork.ProductRepository.GetByProductCode(orderDto.ProductCode);

            if (product == null)
                throw new Exception(MessageConstants.ProductNotFoundError);

            if (product.Stock < orderDto.Quantity || orderDto.Quantity <= 0)
                throw new Exception(MessageConstants.StockQuantityError);

            var appCampaignInfo = await ApplyCampaignToPrice(product, orderDto.Quantity);

            var order = Order.Create(Guid.NewGuid(), orderDto.ProductCode, appCampaignInfo.PromotedPrice, appCampaignInfo.Name, orderDto.Quantity);

            _unitOfWork.OrderRepository.Create(order);
            _unitOfWork.SaveChanges();

            product.DecreaseStockQuantityByQuantity(orderDto.Quantity);

            return orderDto;
        }

        [LoggerAspect]
        public virtual async Task<AppliedCampaignInfoDto>  ApplyCampaignToPrice(Product product, int quantity)
        {
            var appCampaignInfo = new AppliedCampaignInfoDto();

            var activeCampaignDomain = _unitOfWork.CampaignRepository.GetByProductCode(product.ProductCode);

            if (activeCampaignDomain == null)
                return appCampaignInfo;

            appCampaignInfo.PromotedPrice = activeCampaignDomain.ApplyCampaignToPrice(product.Price);
            appCampaignInfo.Name = activeCampaignDomain.Name;

            activeCampaignDomain.IncreaseCampaignSalesCountByQuantity(quantity);

            return appCampaignInfo;
        }


    }
}

