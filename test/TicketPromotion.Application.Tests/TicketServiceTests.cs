using System;
using AutoMapper;
using TicketTypePromotion.Application.Common.Interfaces;
using TicketTypePromotion.Domain.TicketTypes;
using TicketTypePromotion.Domain.SeedWork;
using Moq;
using Xunit;
using TicketTypePromotion.Application.TicketTypeServices;

namespace HepsiPromotion.Application.Tests
{
    public class TicketTypeServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<TicketTypeService> _ticketService;

        public TicketTypeServiceTests()
        {
            var mockedTicketTypeRepository = new Mock<ITicketTypeRepository>();
            var mockedReservationRepository = new Mock<IReservationRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository).Returns(mockedTicketTypeRepository.Object);
            _mockUnitOfWork.Setup(x => x.ReservationRepository).Returns(mockedReservationRepository.Object);
            var mockMapper = new Mock<IMapper>();

            _ticketService = new Mock<TicketTypeService>(_mockUnitOfWork.Object, mockMapper.Object)
            {
                CallBase = true
            };
        }

        #region CreateTicket
        [Fact]
        public void CreateTicket_WithValidParameters_CreatesTicket()
        {
            //Arrange
            var ticketDto = new TicketTypeDto { TicketTypeCode = "a123", Stock = 5, Price = 10 };
            var expectedTicketTypeDto = new TicketTypeDto { TicketTypeCode = "a123", Stock = 5, Price = 10 };
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(It.IsAny<string>())).Returns((TicketType)null);
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.Create(It.IsAny<TicketType>()));
            _mockUnitOfWork.Setup(x => x.SaveChanges());

            //Act
            var actualTicket = _ticketService.Object.CreateTicketType(ticketDto);

            //Assert
            Assert.Equal(ticketDto, actualTicket);
        }

        [Fact]
        public void CreateTicket_WithNullTicketTypeDto_ThrowsException()
        {
            //Arrange
            TicketTypeDto ticketDto = null;


            //Act
            var actualException = Assert.Throws<Exception>(() => _ticketService.Object.CreateTicketType(ticketDto));


            //Assert
            Assert.Equal(MessageConstants.NullParameterError, actualException.Message);

        }

        [Fact]
        public void CreateTicket_WithDuplicateTicketTypeCode_ThrowsException()
        {
            //Arrange
            var ticketDto = new TicketTypeDto { TicketTypeCode = "a123", Stock = 5, Price = 10 };
            var ticketDomainTestData = TicketType.Create("a123", 3, 11);
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(ticketDto.TicketTypeCode)).Returns(ticketDomainTestData);

            //Act
            var actualException = Assert.Throws<Exception>(() => _ticketService.Object.CreateTicketType(ticketDto));


            //Assert
            Assert.Equal(MessageConstants.DuplicateTicketTypeError, actualException.Message);

        }

        [Fact]
        public void CreateTicket_WithValidParameters_ShouldGoUpToSaveChanges()
        {
            //Arrange
            var ticketDto = new TicketTypeDto { TicketTypeCode = "a123", Stock = 5, Price = 10 };
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(It.IsAny<string>())).Returns((TicketType)null);
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.Create(It.IsAny<TicketType>()));

            //Act
            _ticketService.Object.CreateTicketType(ticketDto);

            //Assert
            _mockUnitOfWork.Verify(u => u.TicketTypeRepository.Create(It.IsAny<TicketType>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Once);

        }

        [Fact]
        public void CreateTicket_WithValidParameters_ShouldReturnMyObjectWithoutManipulating()
        {
            //Arrange
            var ticketDto = new TicketTypeDto { TicketTypeCode = "a123", Stock = 5, Price = 10 };
            var expectedTicketTypeDto = new TicketTypeDto { TicketTypeCode = "a123", Stock = 5, Price = 10 };
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(It.IsAny<string>())).Returns((TicketType)null);
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.Create(It.IsAny<TicketType>()));
            _mockUnitOfWork.Setup(x => x.SaveChanges());

            //Act
            _ticketService.Object.CreateTicketType(ticketDto);

            //Assert
            Assert.Equal(expectedTicketTypeDto.Price, ticketDto.Price);
            Assert.Equal(expectedTicketTypeDto.TicketTypeCode, ticketDto.TicketTypeCode);
            Assert.Equal(expectedTicketTypeDto.Stock, ticketDto.Stock);

        }

        #endregion

        #region GetTicket

        [Fact]
         public void GetTicket_WithNonExistingTicket_ThrowsException()
        {
            //Arrange
            var ticketCode = "a123";
            _mockUnitOfWork.Setup(x => x.TicketTypeRepository.GetByTicketTypeCode(ticketCode)).Returns((TicketType)null);

            //Act
            var actualException = Assert.Throws<Exception>(() => _ticketService.Object.GetTicketType(ticketCode));

            //Assert
            Assert.Equal(MessageConstants.NullParameterError, actualException.Message);

        }

        #endregion
    }
}
