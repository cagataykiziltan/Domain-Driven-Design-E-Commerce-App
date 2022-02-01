
using System;
using AutoMapper;
using Ecommence.Application.Common.Interfaces;
using Ecommence.Application.ProductServices;
using Ecommence.Domain.Products;
using Ecommence.Domain.SeedWork;
using Moq;
using Xunit;

namespace HepsiCampaign.Application.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ProductService> _productService;

        public ProductServiceTests()
        {
            var mockedProductRepository = new Mock<IProductRepository>();
            var mockedOrderRepository = new Mock<IOrderRepository>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(x => x.ProductRepository).Returns(mockedProductRepository.Object);
            _mockUnitOfWork.Setup(x => x.OrderRepository).Returns(mockedOrderRepository.Object);
            var mockMapper = new Mock<IMapper>();

            _productService = new Mock<ProductService>(_mockUnitOfWork.Object, mockMapper.Object)
            {
                CallBase = true
            };
        }

        #region CreateProduct
        [Fact]
        public void CreateProduct_WithValidParameters_CreatesProduct()
        {
            //Arrange
            var productDto = new ProductDto { ProductCode = "a123", Stock = 5, Price = 10 };
            var expectedProductDto = new ProductDto { ProductCode = "a123", Stock = 5, Price = 10 };
            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(It.IsAny<string>())).Returns((Product)null);
            _mockUnitOfWork.Setup(x => x.ProductRepository.Create(It.IsAny<Product>()));
            _mockUnitOfWork.Setup(x => x.SaveChanges());

            //Act
            var actualProduct = _productService.Object.CreateProduct(productDto);

            //Assert
            Assert.Equal(productDto, actualProduct);
        }

        [Fact]
        public void CreateProduct_WithNullProductDto_ThrowsException()
        {
            //Arrange
            ProductDto productDto = null;


            //Act
            var actualException = Assert.Throws<Exception>(() => _productService.Object.CreateProduct(productDto));


            //Assert
            Assert.Equal(MessageConstants.NullParameterError, actualException.Message);

        }

        [Fact]
        public void CreateProduct_WithDuplicateProductCode_ThrowsException()
        {
            //Arrange
            var productDto = new ProductDto { ProductCode = "a123", Stock = 5, Price = 10 };
            var productDomainTestData = Product.Create("a123", 3, 11);
            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(productDto.ProductCode)).Returns(productDomainTestData);

            //Act
            var actualException = Assert.Throws<Exception>(() => _productService.Object.CreateProduct(productDto));


            //Assert
            Assert.Equal(MessageConstants.DuplicateProductError, actualException.Message);

        }

        [Fact]
        public void CreateProduct_WithValidParameters_ShouldGoUpToSaveChanges()
        {
            //Arrange
            var productDto = new ProductDto { ProductCode = "a123", Stock = 5, Price = 10 };
            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(It.IsAny<string>())).Returns((Product)null);
            _mockUnitOfWork.Setup(x => x.ProductRepository.Create(It.IsAny<Product>()));

            //Act
            _productService.Object.CreateProduct(productDto);

            //Assert
            _mockUnitOfWork.Verify(u => u.ProductRepository.Create(It.IsAny<Product>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChanges(), Times.Once);

        }

        [Fact]
        public void CreateProduct_WithValidParameters_ShouldReturnMyObjectWithoutManipulating()
        {
            //Arrange
            var productDto = new ProductDto { ProductCode = "a123", Stock = 5, Price = 10 };
            var expectedProductDto = new ProductDto { ProductCode = "a123", Stock = 5, Price = 10 };
            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(It.IsAny<string>())).Returns((Product)null);
            _mockUnitOfWork.Setup(x => x.ProductRepository.Create(It.IsAny<Product>()));
            _mockUnitOfWork.Setup(x => x.SaveChanges());

            //Act
            _productService.Object.CreateProduct(productDto);

            //Assert
            Assert.Equal(expectedProductDto.Price, productDto.Price);
            Assert.Equal(expectedProductDto.ProductCode, productDto.ProductCode);
            Assert.Equal(expectedProductDto.Stock, productDto.Stock);

        }

        #endregion

        #region GetProduct

        [Fact]
         public void GetProduct_WithNonExistingProduct_ThrowsException()
        {
            //Arrange
            var productCode = "a123";
            _mockUnitOfWork.Setup(x => x.ProductRepository.GetByProductCode(productCode)).Returns((Product)null);

            //Act
            var actualException = Assert.Throws<Exception>(() => _productService.Object.GetProduct(productCode));

            //Assert
            Assert.Equal(MessageConstants.NullParameterError, actualException.Message);

        }

        #endregion
    }
}
