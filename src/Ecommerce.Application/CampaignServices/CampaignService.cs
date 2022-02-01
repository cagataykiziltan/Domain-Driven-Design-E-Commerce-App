using System;
using AutoMapper;
using Ecommence.Domain.Campaigns;
using System.Linq;
using Ecommence.Application.Common.Aspects;
using Ecommence.Application.Common.Interfaces;
using Ecommence.Domain.SeedWork;
using System.Threading.Tasks;

namespace Ecommence.Application.CampaignServices
{
    public class CampaignService : ICampaignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CampaignService(IUnitOfWork unitOfWork,
                               IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [LoggerAspect]
        //[TransactionAspect]
        public async Task<CampaignDto> CreateCampaign(CampaignDto campaignDto)
        {
            if (campaignDto == null)
                throw new Exception(MessageConstants.NullParameterError);

            var product = _unitOfWork.ProductRepository.GetByProductCode(campaignDto.ProductCode);

            if (product == null)
                throw new Exception(MessageConstants.ProductNotFoundError);

            var isThereAlreadyCampaign = _unitOfWork.CampaignRepository.CheckExistingCampaignByProductCodeAndName(campaignDto.ProductCode, campaignDto.Name);

            if (isThereAlreadyCampaign)
                throw new Exception(MessageConstants.ExistingCampaignError);

            var campaignDomain = Campaign.Create(Guid.NewGuid(), 
                                                 campaignDto.Name, 
                                                 campaignDto.Duration,
                                                 campaignDto.ProductCode,
                                                 campaignDto.PriceManipulationLimit,
                                                 campaignDto.TargetSalesCount);

            _unitOfWork.CampaignRepository.Create(campaignDomain);
            _unitOfWork.SaveChanges();

            return campaignDto;
        }

        [LoggerAspect]
        public async Task<CampaignDto> GetCampaignByName(string campaignName)
        {
            var campaign = _unitOfWork.CampaignRepository.GetByName(campaignName);

            if (campaign == null)
                throw new Exception(MessageConstants.CampaignNotFoundError);

            var orderDomainList = _unitOfWork.OrderRepository.GetOrdersByCampaignCode(campaign.Name);

            if (orderDomainList.Any())
                campaign.SetAveragePriceValue(orderDomainList.Average(x => x.Product.Price * x.Quantity));

            var campaignDto = _mapper.Map<CampaignDto>(campaign);

            return campaignDto;
        }
    }
}
