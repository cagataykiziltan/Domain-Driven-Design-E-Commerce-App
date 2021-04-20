using System;
using TicketTypePromotion.Application.Common.Aspects;
using TicketTypePromotion.Application.Common.Interfaces;
using TicketTypePromotion.Domain.Reservations;
using TicketTypePromotion.Domain.TicketTypes;
using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Application.ReservationServices
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [LoggerAspect]
        [TransactionAspect]
        public ReservationDto CreateReservation(ReservationDto orderDto)
        {
            if (orderDto == null)
                throw new Exception(MessageConstants.NullParameterError);

            var ticketType = _unitOfWork.TicketTypeRepository.GetByTicketTypeCode(orderDto.TicketTypeCode);

            if (ticketType == null)
                throw new Exception(MessageConstants.TicketTypeNotFoundError);

            if (ticketType.Stock < orderDto.Quantity || orderDto.Quantity <= 0)
                throw new Exception(MessageConstants.StockQuantityError);

            var appPromotionInfo = ApplyPromotionToPrice(ticketType, orderDto.Quantity);

            var order = Reservation.Create(orderDto.TicketTypeCode, appPromotionInfo.PromotedPrice, appPromotionInfo.Name, orderDto.Quantity);

            _unitOfWork.ReservationRepository.Create(order);
            _unitOfWork.SaveChanges();

            ticketType.DecreaseStockQuantityByQuantity(orderDto.Quantity);

            return orderDto;
        }

        [LoggerAspect]
        public virtual AppliedPromotionInfoDto ApplyPromotionToPrice(TicketType ticketType, int quantity)
        {
            var appPromotionInfo = new AppliedPromotionInfoDto();

            var activePromotionDomain = _unitOfWork.PromotionRepository.GetByTicketTypeCode(ticketType.TicketTypeCode);

            if (activePromotionDomain == null)
                return appPromotionInfo;

            appPromotionInfo.PromotedPrice = activePromotionDomain.ApplyPromotionToPrice(ticketType.Price);
            appPromotionInfo.Name = activePromotionDomain.Name;

            activePromotionDomain.IncreasePromotionSalesCountByQuantity(quantity);

            return appPromotionInfo;
        }


    }
}

