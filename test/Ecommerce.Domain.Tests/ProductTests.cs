using Ecommence.Domain.Products;
using Ecommence.Domain.SeedWork;
using Xunit;

namespace HepsiCampaign.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void ProductDomainCreate_WithGivenCorrectValues_CreatesCorrectProduct()
        {
            //Arrange
            const string productCode = "A1234";
            const int price = 5;
            const int stock = 5;
            
            //Act
            var product = Product.Create(productCode, price, stock);

            //Assert
            Assert.Equal(productCode, product.ProductCode);
            Assert.Equal(price, product.Price);
            Assert.Equal(stock, product.Stock);
        }

        [Fact]
        public void ProductDomainCreate_WithGivenSameValues_CreatesUniqueDifferentEntities()
        {
            //Arrange
            const string productCode = "A1234";
            const int price = 5;
            const int stock = 5;

            //Act
            var firstProduct = Product.Create(productCode, price, stock);
            var secondProduct = Product.Create(productCode, price, stock);

            //Assert
            Assert.NotEqual(firstProduct.Id, secondProduct.Id);
        }

        [Fact]
        public void ProductDomainCreate_WithNullProductCode_ThrowsException()
        {
            //Arrange
            const string productCode = null;
            const int price = 5;
            const int stock = 5;

            //Act 
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => Product.Create(productCode, price, stock));

            //Assert
            Assert.Equal( MessageConstants.NullProductCodeError, actualException.Message);
        }

        [Theory]
        [InlineData(-1)]
        public void ProductDomainCreate_WithInvalidQuantity_ThrowsException(int stock)
        {
            //Arrange
            const string productCode = "A1234";
            const int price = 5;
      
            //Act 
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => Product.Create(productCode, price, stock));

            //Assert
            Assert.Equal(MessageConstants.NegativeOrZeroStockError, actualException.Message);
        }

        [Theory]
        [InlineData(-1)]
        public void ProductDomainCreate_WithInvalidPrice_ThrowsException(int price)
        {
            //Arrange
            const string productCode = "A1234";
            const int stock = 5;

            //Act 
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => Product.Create(productCode, price, stock));


            //Assert
            Assert.Equal(MessageConstants.NegativeOrZeroPriceError, actualException.Message);
        }

   
        [Fact]
        public void SetPromotedPrice_WithCorrectValue_SetCorrectly()
        {
            //Arrange
            const string productCode = "A1234";
            const int price = 5;
            const int stock = 5;
            const int promotedPrice = 7;
            var product = Product.Create(productCode, price, stock);

            //Act
            product.SetPromotedPrice(promotedPrice);

            //Assert
            Assert.Equal(promotedPrice,product.PromotedPrice);

        }

        [Theory]
        [InlineData(-1)]
        public void SetPromotedPrice_WithInvalidValue_ThrowsException(int promotedPrice)
        {
            //Arrange
            const string productCode = "A1234";
            const int price = 5;
            const int stock = 5;
            var product = Product.Create(productCode, price, stock);

            //Act
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => product.SetPromotedPrice(promotedPrice));
           

            //Assert
            Assert.Equal(MessageConstants.PromotedPriceError, actualException.Message);

        }
        
        
        [Fact]
        public void DecreaseStockQuantityByQuantity_WithCorrectValue_DecreasesCorrectly()
        {
            //Arrange
            const string productCode = "A1234";
            const int price = 5;
            const int stock = 5;
            const int quantity = 3;
            var product = Product.Create(productCode, price, stock);
            const int expectedStock = 2;

            //Act
            product.DecreaseStockQuantityByQuantity(quantity);


            //Assert
            Assert.Equal(expectedStock,product.Stock);

        }
    }
}
