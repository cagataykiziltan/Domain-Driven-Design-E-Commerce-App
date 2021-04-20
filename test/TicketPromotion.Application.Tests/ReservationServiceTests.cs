using System;
using TicketTypePromotion.Application.Common.Interfaces;
using TicketTypePromotion.Domain.Promotions;
using TicketTypePromotion.Domain.Reservations;
using TicketTypePromotion.Domain.TicketTypes;
using TicketTypePromotion.Domain.SeedWork;
using Moq;
using Xunit;
using TicketTypePromotion.Application.ReservationServices;

namespace HepsiPromotion.Application.Tests
{
    public class ReservationServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ReservationService> _orderService;
        public ReservationServiceTests()
        {

            var mockedTicketTypeRepository = new Mock<ITicketTypeRepository>();
            var mockedReservationRepository = new Mock<IReservationRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository).Returns(mockedTicketTypeRepository.Object);
            _mockUnitOfWork.Setup(x => x.ReservationRepository).Returns(mockedReservationRepository.Object);

            _orderService = new Mock<ReservationService>(_mockUnitOfWork.Object)
            {
                CallBase = true
            };
        }

        #region CreatePromotion

        [Fact]
        public void CreateReservation_WithValidParameters_CreatesReservation()
        {
            //Arrange
            var orderDto = new ReservationDto { TicketTypeCode = "a123", Quantity = 5 };
            var ticketDomainTestData = TicketType.Create("a123", 5, 5);
            var orderDomainTestData = Reservation.Create(orderDto.TicketTypeCode, 5, "testPromotion", 5);

            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(It.IsAny<string>())).Returns(ticketDomainTestData);
            _orderService.Setup(x => x.ApplyPromotionToPrice(ticketDomainTestData, It.IsAny<int>())).Returns(new AppliedPromotionInfoDto() { Name = "testCampaing", PromotedPrice = 5 });
            _mockUnitOfWork.Setup(x => x.ReservationRepository.Create(orderDomainTestData));

            //Act
            var actualReservation = _orderService.Object.CreateReservation(orderDto);

            //Assert
            Assert.Same(orderDto, actualReservation);
        }

        [Fact]
        public void CreateReservation_WithNullParameter_ThrowsException()
        {
            //Arrange
            ReservationDto orderDto = null;

            //Act
            var actualException = Assert.Throws<Exception>(() => _orderService.Object.CreateReservation(orderDto));

            //Assert
            Assert.Equal(MessageConstants.NullParameterError, actualException.Message);

        }

        [Fact]
        public void CreateReservation_WithNotFoundReservationedTicket_ThrowsException()
        {
            //Arrange
            var orderDto = new ReservationDto { TicketTypeCode = "a123", Quantity = 5 };
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(It.IsAny<string>())).Returns((TicketType)null);

            //Act
            var actualException = Assert.Throws<Exception>(() => _orderService.Object.CreateReservation(orderDto));


            //Assert
            Assert.Equal(MessageConstants.TicketTypeNotFoundError, actualException.Message);

        }

        [Fact]
        public void CreateReservation_WithStockLessThanDemandedQuantity_ThrowsException()
        {
            //Arrange
            var orderDto = new ReservationDto { TicketTypeCode = "a123", Quantity = 9 };
            var ticketDomainTestData = TicketType.Create("a123", 5, 5);
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(It.IsAny<string>())).Returns(ticketDomainTestData);

            //Act
            var actualException = Assert.Throws<Exception>(() => _orderService.Object.CreateReservation(orderDto));


            //Assert
            Assert.Equal(MessageConstants.StockQuantityError, actualException.Message);

        }


        [Fact]
        public void CreateReservation_WithZeroQuantity_ThrowsException()
        {
            //Arrange
            var ticketDomainTest = TicketType.Create("a123", 5, 5);
            var reservationDto = new ReservationDto { TicketTypeCode = "a123", Quantity = 0 };
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(It.IsAny<string>())).Returns(ticketDomainTest);

            //Act
            var actualException = Assert.Throws<Exception>(() => _orderService.Object.CreateReservation(reservationDto));


            //Assert
            Assert.Equal(MessageConstants.StockQuantityError, actualException.Message);

        }

        [Fact]
        public void CreateReservation_WithValidParameters_ShouldGoUpToSaveChanges()
        {
            //Arrange
            var reservationDto = new ReservationDto { TicketTypeCode = "a123", Quantity = 5 };
            var ticketDomainTestData = TicketType.Create("a123", 5, 5);
            var orderDomainTestData = Reservation.Create(reservationDto.TicketTypeCode, 5, "testPromotion", 5);

            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(It.IsAny<string>())).Returns(ticketDomainTestData);
            _orderService.Setup(x => x.ApplyPromotionToPrice(ticketDomainTestData, It.IsAny<int>())).Returns(new AppliedPromotionInfoDto() { Name = "testCampaing", PromotedPrice = 5 });
            _mockUnitOfWork.Setup(x => x.ReservationRepository.Create(orderDomainTestData));

            //Act
            _orderService.Object.CreateReservation(reservationDto);

            //Assert
            _mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }



        #endregion

        #region ApplyPromotionToPrice

        [Fact]
        public void ApplyPromotionToPrice_WithNoPromotion_ReturnsTrueObject()
        {
            //Arrange
            var ticketDomainTestData = TicketType.Create("a123", 5, 5);
            var promotionDomainTestData = Promotion.Create("testPromotion", 5, "a123", 20, 100);
            const int quantity = 7;
            _mockUnitOfWork.Setup(x => x.PromotionRepository.GetByTicketTypeCode(ticketDomainTestData.TicketTypeCode)).Returns(promotionDomainTestData);


            //Act
            var actualResult = _orderService.Object.ApplyPromotionToPrice(ticketDomainTestData, quantity);


            //Assert
            Assert.NotEqual(0, actualResult.PromotedPrice);
            Assert.NotNull(actualResult.Name);
        }

        [Fact]
        public void ApplyPromotionToPrice_WithNoPromotion_ReturnsEmptyObject()
        {
            //Arrange
            var ticketDomainTestData = TicketType.Create("a123", 5, 5);
            const int quantity = 7;
            _mockUnitOfWork.Setup(x => x.PromotionRepository.GetByTicketTypeCode(ticketDomainTestData.TicketTypeCode)).Returns((Promotion)null);

            //Act
            var actualResult = _orderService.Object.ApplyPromotionToPrice(ticketDomainTestData, quantity);

            //Assert
            Assert.Equal(0, actualResult.PromotedPrice);
            Assert.Null(actualResult.Name);
        }

        #endregion


    }
}
