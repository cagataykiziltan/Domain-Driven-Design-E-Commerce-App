using Moq;
using System;
using Ecommence.Application.Common.Interfaces;
using Ecommence.Application.OrderServices;
using Ecommence.Domain.Campaigns;
using Ecommence.Domain.Orders;
using Ecommence.Domain.Products;
using Ecommence.Domain.SeedWork;
using Xunit;

namespace HepsiCampaign.Application.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<OrderService> _orderService;
        public OrderServiceTests()
        {

            var mockedProductRepository = new Mock<IProductRepository>();
            var mockedOrderRepository = new Mock<IOrderRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(x => x.ProductRepository).Returns(mockedProductRepository.Object);
            _mockUnitOfWork.Setup(x => x.OrderRepository).Returns(mockedOrderRepository.Object);

            _orderService = new Mock<OrderService>(_mockUnitOfWork.Object)
            {
                CallBase = true
            };
        }

        #region CreateOrder

        [Fact]
        public void CreateOrder_WithValidParameters_CreatesOrder()
        {
            //Arrange
            var orderDto = new OrderDto { ProductCode = "a123", Quantity = 5 };
            var productDomainTestData = Product.Create("a123", 5, 5);
            var orderDomainTestData = Order.Create(orderDto.ProductCode, 5, "testCampaign", 5);

            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(It.IsAny<string>())).Returns(productDomainTestData);
            _orderService.Setup(x => x.ApplyCampaignToPrice(productDomainTestData, It.IsAny<int>())).Returns(new AppliedCampaignInfoDto() { Name = "testCampaing", PromotedPrice = 5 });
            _mockUnitOfWork.Setup(x => x.OrderRepository.Create(orderDomainTestData));

            //Act
            var actualOrder = _orderService.Object.CreateOrder(orderDto);

            //Assert
            Assert.Same(orderDto, actualOrder);
        }

        [Fact]
        public void CreateOrder_WithNullParameter_ThrowsException()
        {
            //Arrange
            OrderDto orderDto = null;

            //Act
            var actualException = Assert.Throws<Exception>(() => _orderService.Object.CreateOrder(orderDto));

            //Assert
            Assert.Equal(MessageConstants.NullParameterError, actualException.Message);

        }

        [Fact]
        public void CreateOrder_WithNotFoundOrderedProduct_ThrowsException()
        {
            //Arrange
            var orderDto = new OrderDto { ProductCode = "a123", Quantity = 5 };
            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(It.IsAny<string>())).Returns((Product)null);

            //Act
            var actualException = Assert.Throws<Exception>(() => _orderService.Object.CreateOrder(orderDto));


            //Assert
            Assert.Equal(MessageConstants.ProductNotFoundError, actualException.Message);

        }

        [Fact]
        public void CreateOrder_WithStockLessThanDemandedQuantity_ThrowsException()
        {
            //Arrange
            var orderDto = new OrderDto { ProductCode = "a123", Quantity = 9 };
            var productDomainTestData = Product.Create("a123", 5, 5);
            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(It.IsAny<string>())).Returns(productDomainTestData);

            //Act
            var actualException = Assert.Throws<Exception>(() => _orderService.Object.CreateOrder(orderDto));


            //Assert
            Assert.Equal(MessageConstants.StockQuantityError, actualException.Message);

        }


        [Fact]
        public void CreateOrder_WithZeroQuantity_ThrowsException()
        {
            //Arrange
            var productDomainTest = Product.Create("a123", 5, 5);
            var orderDto = new OrderDto { ProductCode = "a123", Quantity = 0 };
            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(It.IsAny<string>())).Returns(productDomainTest);

            //Act
            var actualException = Assert.Throws<Exception>(() => _orderService.Object.CreateOrder(orderDto));


            //Assert
            Assert.Equal(MessageConstants.StockQuantityError, actualException.Message);

        }

        [Fact]
        public void CreateOrder_WithValidParameters_ShouldGoUpToSaveChanges()
        {
            //Arrange
            var orderDto = new OrderDto { ProductCode = "a123", Quantity = 5 };
            var productDomainTestData = Product.Create("a123", 5, 5);
            var orderDomainTestData = Order.Create(orderDto.ProductCode, 5, "testCampaign", 5);

            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(It.IsAny<string>())).Returns(productDomainTestData);
            _orderService.Setup(x => x.ApplyCampaignToPrice(productDomainTestData, It.IsAny<int>())).Returns(new AppliedCampaignInfoDto() { Name = "testCampaing", PromotedPrice = 5 });
            _mockUnitOfWork.Setup(x => x.OrderRepository.Create(orderDomainTestData));

            //Act
            _orderService.Object.CreateOrder(orderDto);

            //Assert
            _mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }



        #endregion

        #region ApplyCampaignToPrice

        [Fact]
        public void ApplyCampaignToPrice_WithNoCampaign_ReturnsTrueObject()
        {
            //Arrange
            var productDomainTestData = Product.Create("a123", 5, 5);
            var campaignDomainTestData = Campaign.Create("testCampaign", 5, "a123", 20, 100);
            const int quantity = 7;
            _mockUnitOfWork.Setup(x => x.CampaignRepository.GetByProductCode(productDomainTestData.ProductCode)).Returns(campaignDomainTestData);


            //Act
            var actualResult = _orderService.Object.ApplyCampaignToPrice(productDomainTestData, quantity);


            //Assert
            Assert.NotEqual(0, actualResult.PromotedPrice);
            Assert.NotNull(actualResult.Name);
        }

        [Fact]
        public void ApplyCampaignToPrice_WithNoCampaign_ReturnsEmptyObject()
        {
            //Arrange
            var productDomainTestData = Product.Create("a123", 5, 5);
            const int quantity = 7;
            _mockUnitOfWork.Setup(x => x.CampaignRepository.GetByProductCode(productDomainTestData.ProductCode)).Returns((Campaign)null);

            //Act
            var actualResult = _orderService.Object.ApplyCampaignToPrice(productDomainTestData, quantity);

            //Assert
            Assert.Equal(0, actualResult.PromotedPrice);
            Assert.Null(actualResult.Name);
        }

        #endregion


    }
}
