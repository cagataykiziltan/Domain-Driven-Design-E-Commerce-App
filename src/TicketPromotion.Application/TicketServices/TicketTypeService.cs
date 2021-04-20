using System;
using AutoMapper;
using TicketTypePromotion.Application.Common.Aspects;
using TicketTypePromotion.Application.Common.Interfaces;
using TicketTypePromotion.Domain.TicketTypes;
using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Application.TicketTypeServices
{
    public class TicketTypeService : ITicketTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TicketTypeService(IUnitOfWork unitOfWork,
                              IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [LoggerAspect]
        [TransactionAspect]
        public TicketTypeDto CreateTicketType(TicketTypeDto ticketDto)
        {
            if (ticketDto == null)
                 throw new Exception(MessageConstants.NullParameterError);

            var existingTicketType =  _unitOfWork.TicketTypeRepository.GetByTicketTypeCode(ticketDto.TicketTypeCode);

            if (existingTicketType != null)
                throw new Exception(MessageConstants.DuplicateTicketTypeError);

            var ticketType = TicketType.Create(ticketDto.TicketTypeCode, ticketDto.Price, ticketDto.Stock);

            _unitOfWork.TicketTypeRepository.Create(ticketType);
            _unitOfWork.SaveChanges();
        
            return ticketDto;
        }

      
        [LoggerAspect]
         public TicketTypeDto GetTicketType(string ticketCode)
        {
            var ticketType = _unitOfWork.TicketTypeRepository.GetByTicketTypeCode(ticketCode);

            if (ticketType == null)
                throw new Exception(MessageConstants.NullParameterError);

            var promotion = _unitOfWork.PromotionRepository.GetByTicketTypeCode(ticketCode);

            if (promotion != null)
            {
                var promotedPrice = promotion.ApplyPromotionToPrice(ticketType.Price);
                ticketType.SetPromotedPrice(promotedPrice);
            }

            var ticketDto = _mapper.Map<TicketTypeDto>(ticketType);
            
            return ticketDto;
        }
    }
}
