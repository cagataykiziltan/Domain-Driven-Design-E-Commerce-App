using System;
using System.Collections.Generic;
using AutoMapper;
using TicketTypePromotion.Application.PromotionServices;
using TicketTypePromotion.Application.Common.Interfaces;
using TicketTypePromotion.Domain.Promotions;
using TicketTypePromotion.Domain.Reservations;
using TicketTypePromotion.Domain.TicketTypes;
using TicketTypePromotion.Domain.SeedWork;
using Moq;
using Xunit;

namespace HepsiPromotion.Application.Tests
{
    public class PromotionServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<PromotionService> _promotionService;
        public PromotionServiceTests()
        {

            var mockedTicketTypeRepository = new Mock<ITicketTypeRepository>();
            var mockedReservationRepository = new Mock<IReservationRepository>();
            var mockedPromotionRepository = new Mock<IPromotionRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository).Returns(mockedTicketTypeRepository.Object);
            _mockUnitOfWork.Setup(x => x.ReservationRepository).Returns(mockedReservationRepository.Object);
            _mockUnitOfWork.Setup(x => x.PromotionRepository).Returns(mockedPromotionRepository.Object);

            var mockMapper = new Mock<IMapper>();

            _promotionService = new Mock<PromotionService>(_mockUnitOfWork.Object, mockMapper.Object)
            {
                CallBase = true
            };

        }

        #region CreatePromotion
        [Fact]
        public void CreatePromotion_WithValidParameters_ReturnsPromotion()
        {
            //Arrange
            var promotionDto = new PromotionDto { Name = "testPromotion", Duration = 5, TicketTypeCode = "a123", PriceManipulationLimit = 20, TargetSalesCount = 100 };
            var ticketDomainTestData = TicketType.Create("a123", 5, 5);

            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(It.IsAny<string>())).Returns(ticketDomainTestData);
            _mockUnitOfWork.Setup(x => x.PromotionRepository.CheckExistingPromotionByTicketTypeCodeAndName(promotionDto.TicketTypeCode, promotionDto.Name)).Returns(false);


            //Act
            var actualResult = _promotionService.Object.CreatePromotion(promotionDto);

            //Assert
            Assert.Same(promotionDto, actualResult);
        }

        [Fact]
        public void CreatePromotion_WithNoParameter_ThrowsException()
        {
            //Arrange
            PromotionDto promotionDto = null;

            //Act
            var actualException = Assert.Throws<Exception>(() => _promotionService.Object.CreatePromotion(promotionDto));

            //Assert
            Assert.Equal(MessageConstants.NullParameterError, actualException.Message);

        }

        [Fact]
        public void CreatePromotion_WithNotExistingPromotionTicket_ThrowsException()
        {
            //Arrange
            var promotionDto = new PromotionDto { Name = "testPromotion", Duration = 5, TicketTypeCode = "a123", PriceManipulationLimit = 20, TargetSalesCount = 100 };
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(promotionDto.TicketTypeCode)).Returns((TicketType)null);

            //Act
            var actualException = Assert.Throws<Exception>(() => _promotionService.Object.CreatePromotion(promotionDto));

            //Assert
            Assert.Equal(MessageConstants.TicketTypeNotFoundError, actualException.Message);

        }

        [Fact]
        public void CreatePromotion_WithExistingPromotionName_ThrowsException()
        {
            //Arrange
            var promotionDto = new PromotionDto { Name = "testPromotion", Duration = 5, TicketTypeCode = "a123", PriceManipulationLimit = 20, TargetSalesCount = 100 };
            var ticketDomainTestData = TicketType.Create("a123", 5, 5);
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(promotionDto.TicketTypeCode)).Returns(ticketDomainTestData);
            _mockUnitOfWork.Setup(x => x.PromotionRepository.CheckExistingPromotionByTicketTypeCodeAndName(promotionDto.TicketTypeCode, promotionDto.Name)).Returns(true);

            //Act
            var actualException = Assert.Throws<Exception>(() => _promotionService.Object.CreatePromotion(promotionDto));

            //Assert
            Assert.Equal(MessageConstants.ExistingPromotionError, actualException.Message);

        }

        [Fact]
        public void CreatePromotion_WithValidParameters_ShouldGoUpToCreateAndSaveChanges()
        {
            //Arrange
            var promotionDto = new PromotionDto { Name = "testPromotion", Duration = 5, TicketTypeCode = "a123", PriceManipulationLimit = 20, TargetSalesCount = 100 };
            var ticketDomainTestData = TicketType.Create("a123", 5, 5);

            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(It.IsAny<string>())).Returns(ticketDomainTestData);
            _mockUnitOfWork.Setup(x => x.PromotionRepository.CheckExistingPromotionByTicketTypeCodeAndName(promotionDto.TicketTypeCode, promotionDto.Name)).Returns(false);


            //Act
            _promotionService.Object.CreatePromotion(promotionDto);

            //Assert
            _mockUnitOfWork.Verify(u => u.PromotionRepository.Create(It.IsAny<Promotion>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }

        #endregion


        #region GetPromotionByName

        [Fact]
        public void GetPromotionByName_WithNoExistingPromotion_ThrowsException()
        {
            //Arrange
            const string promotionName = "testPromotion";
            _mockUnitOfWork.Setup(x => x.PromotionRepository.GetByName(promotionName)).Returns((Promotion)null);


            //Act
            var actualException = Assert.Throws<Exception>(() => _promotionService.Object.GetPromotionByName(promotionName));


            //Assert
            Assert.Equal(MessageConstants.PromotionNotFoundError, actualException.Message);
        }

        [Fact]
        public void GetPromotionByName_WithExistingReservationList_CalculatesAveragePriceValue()
        {
            //Arrange
            const string promotionName = "testPromotion";
            var promotionDomain = Promotion.Create("testPromotion", 5, "a123", 20, 100);
            _mockUnitOfWork.Setup(x => x.PromotionRepository.GetByName(promotionName)).Returns(promotionDomain);
            _mockUnitOfWork.Setup(x => x.ReservationRepository.GetReservationsByPromotionCode(promotionName))
                 .Returns(new List<Reservation>
                 {
                     Reservation.Create("a123", 5, "testPromotion", 3),
                     Reservation.Create("a123", 5, "testPromotion", 4)
                 });


            //Act
            _promotionService.Object.GetPromotionByName(promotionName);


            //Assert
            Assert.NotEqual(0, promotionDomain.AveragePriceValue);
        }


        #endregion


    }
}
