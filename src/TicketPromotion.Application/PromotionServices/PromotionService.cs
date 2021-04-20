using System;
using AutoMapper;
using TicketTypePromotion.Domain.Promotions;
using System.Linq;
using TicketTypePromotion.Application.Common.Aspects;
using TicketTypePromotion.Application.Common.Interfaces;
using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Application.PromotionServices
{
    public class PromotionService : IPromotionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PromotionService(IUnitOfWork unitOfWork,
                               IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [LoggerAspect]
        [TransactionAspect]
        public PromotionDto CreatePromotion(PromotionDto promotionDto)
        {
            if (promotionDto == null)
                throw new Exception(MessageConstants.NullParameterError);

            var ticketType = _unitOfWork.TicketTypeRepository.GetByTicketTypeCode(promotionDto.TicketTypeCode);

            if (ticketType == null)
                throw new Exception(MessageConstants.TicketTypeNotFoundError);

            var isThereAlreadyPromotion = _unitOfWork.PromotionRepository.CheckExistingPromotionByTicketTypeCodeAndName(promotionDto.TicketTypeCode, promotionDto.Name);

            if (isThereAlreadyPromotion)
                throw new Exception(MessageConstants.ExistingPromotionError);

            var promotionDomain = Promotion.Create(promotionDto.Name, promotionDto.Duration, promotionDto.TicketTypeCode, promotionDto.PriceManipulationLimit, promotionDto.TargetSalesCount);

            _unitOfWork.PromotionRepository.Create(promotionDomain);
            _unitOfWork.SaveChanges();

            return promotionDto;
        }

        [LoggerAspect]
        public PromotionDto GetPromotionByName(string promotionName)
        {
            var promotion = _unitOfWork.PromotionRepository.GetByName(promotionName);

            if (promotion == null)
                throw new Exception(MessageConstants.PromotionNotFoundError);

            var orderDomainList = _unitOfWork.ReservationRepository.GetReservationsByPromotionCode(promotion.Name);

            if (orderDomainList.Any())
                promotion.SetAveragePriceValue(orderDomainList.Average(x => x.TicketType.Price * x.Quantity));

            var promotionDto = _mapper.Map<PromotionDto>(promotion);

            return promotionDto;
        }
    }
}
