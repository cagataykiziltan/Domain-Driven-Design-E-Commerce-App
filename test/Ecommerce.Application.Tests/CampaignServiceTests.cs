using System;
using System.Collections.Generic;
using AutoMapper;
using Ecommence.Application.CampaignServices;
using Ecommence.Application.Common.Interfaces;
using Ecommence.Domain.Campaigns;
using Ecommence.Domain.Orders;
using Ecommence.Domain.Products;
using Ecommence.Domain.SeedWork;
using Moq;
using Xunit;

namespace HepsiCampaign.Application.Tests
{
    public class CampaignServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<CampaignService> _campaignService;
        public CampaignServiceTests()
        {

            var mockedProductRepository = new Mock<IProductRepository>();
            var mockedOrderRepository = new Mock<IOrderRepository>();
            var mockedCampaignRepository = new Mock<ICampaignRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(x => x.ProductRepository).Returns(mockedProductRepository.Object);
            _mockUnitOfWork.Setup(x => x.OrderRepository).Returns(mockedOrderRepository.Object);
            _mockUnitOfWork.Setup(x => x.CampaignRepository).Returns(mockedCampaignRepository.Object);

            var mockMapper = new Mock<IMapper>();

            _campaignService = new Mock<CampaignService>(_mockUnitOfWork.Object, mockMapper.Object)
            {
                CallBase = true
            };

        }

        #region CreateCampaign
        [Fact]
        public void CreateOrder_WithValidParameters_ReturnsCampaign()
        {
            //Arrange
            var campaignDto = new CampaignDto { Name = "testCampaign", Duration = 5, ProductCode = "a123", PriceManipulationLimit = 20, TargetSalesCount = 100 };
            var productDomainTestData = Product.Create("a123", 5, 5);

            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(It.IsAny<string>())).Returns(productDomainTestData);
            _mockUnitOfWork.Setup(x => x.CampaignRepository.CheckExistingCampaignByProductCodeAndName(campaignDto.ProductCode, campaignDto.Name)).Returns(false);


            //Act
            var actualResult = _campaignService.Object.CreateCampaign(campaignDto);

            //Assert
            Assert.Same(campaignDto, actualResult);
        }

        [Fact]
        public void CreateOrder_WithNoParameter_ThrowsException()
        {
            //Arrange
            CampaignDto campaignDto = null;

            //Act
            var actualException = Assert.Throws<Exception>(() => _campaignService.Object.CreateCampaign(campaignDto));

            //Assert
            Assert.Equal(MessageConstants.NullParameterError, actualException.Message);

        }

        [Fact]
        public void CreateOrder_WithNotExistingCampaignProduct_ThrowsException()
        {
            //Arrange
            var campaignDto = new CampaignDto { Name = "testCampaign", Duration = 5, ProductCode = "a123", PriceManipulationLimit = 20, TargetSalesCount = 100 };
            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(campaignDto.ProductCode)).Returns((Product)null);

            //Act
            var actualException = Assert.Throws<Exception>(() => _campaignService.Object.CreateCampaign(campaignDto));

            //Assert
            Assert.Equal(MessageConstants.ProductNotFoundError, actualException.Message);

        }

        [Fact]
        public void CreateOrder_WithExistingCampaignName_ThrowsException()
        {
            //Arrange
            var campaignDto = new CampaignDto { Name = "testCampaign", Duration = 5, ProductCode = "a123", PriceManipulationLimit = 20, TargetSalesCount = 100 };
            var productDomainTestData = Product.Create("a123", 5, 5);
            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(campaignDto.ProductCode)).Returns(productDomainTestData);
            _mockUnitOfWork.Setup(x => x.CampaignRepository.CheckExistingCampaignByProductCodeAndName(campaignDto.ProductCode, campaignDto.Name)).Returns(true);

            //Act
            var actualException = Assert.Throws<Exception>(() => _campaignService.Object.CreateCampaign(campaignDto));

            //Assert
            Assert.Equal(MessageConstants.ExistingCampaignError, actualException.Message);

        }

        [Fact]
        public void CreateOrder_WithValidParameters_ShouldGoUpToCreateAndSaveChanges()
        {
            //Arrange
            var campaignDto = new CampaignDto { Name = "testCampaign", Duration = 5, ProductCode = "a123", PriceManipulationLimit = 20, TargetSalesCount = 100 };
            var productDomainTestData = Product.Create("a123", 5, 5);

            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(It.IsAny<string>())).Returns(productDomainTestData);
            _mockUnitOfWork.Setup(x => x.CampaignRepository.CheckExistingCampaignByProductCodeAndName(campaignDto.ProductCode, campaignDto.Name)).Returns(false);


            //Act
            _campaignService.Object.CreateCampaign(campaignDto);

            //Assert
            _mockUnitOfWork.Verify(u => u.CampaignRepository.Create(It.IsAny<Campaign>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }

        #endregion


        #region GetCampaignByName

        [Fact]
        public void GetCampaignByName_WithNoExistingCampaign_ThrowsException()
        {
            //Arrange
            const string campaignName = "testCampaign";
            _mockUnitOfWork.Setup(x => x.CampaignRepository.GetByName(campaignName)).Returns((Campaign)null);


            //Act
            var actualException = Assert.Throws<Exception>(() => _campaignService.Object.GetCampaignByName(campaignName));


            //Assert
            Assert.Equal(MessageConstants.CampaignNotFoundError, actualException.Message);
        }

        [Fact]
        public void GetCampaignByName_WithExistingOrderList_CalculatesAveragePriceValue()
        {
            //Arrange
            const string campaignName = "testCampaign";
            var campaignDomain = Campaign.Create("testCampaign", 5, "a123", 20, 100);
            _mockUnitOfWork.Setup(x => x.CampaignRepository.GetByName(campaignName)).Returns(campaignDomain);
            _mockUnitOfWork.Setup(x => x.OrderRepository.GetOrdersByCampaignCode(campaignName))
                 .Returns(new List<Order>
                 {
                     Order.Create("a123", 5, "testCampaign", 3),
                     Order.Create("a123", 5, "testCampaign", 4)
                 });


            //Act
            _campaignService.Object.GetCampaignByName(campaignName);


            //Assert
            Assert.NotEqual(0, campaignDomain.AveragePriceValue);
        }


        #endregion


    }
}
