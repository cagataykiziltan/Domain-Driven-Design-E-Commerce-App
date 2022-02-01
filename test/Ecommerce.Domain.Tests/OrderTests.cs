using Ecommence.Domain.Orders;
using Ecommence.Domain.SeedWork;
using Xunit;

namespace HepsiCampaign.Domain.Tests
{
    public class OrderTests
    {
        #region Order


        [Fact]
        public void OrderDomainCreate_WithGivenWithSameValues_CreatesUniqueDifferentEntities()
        {
            //Arrange
            const string productCode = "A1234";
            const int price = 5;
            const int quantity = 5;
            const string appliedCampaignName = "opportunity";

            //Act
            var firstOrder = Order.Create(productCode, price, appliedCampaignName, quantity);
            var secondOrder = Order.Create(productCode, price, appliedCampaignName, quantity);

            //Assert
            Assert.NotEqual(firstOrder.Id, secondOrder.Id);
        }

        [Fact]
        public void OrderDomainCreate_WithNullProductCode_ThrowsException()
        {
            //Arrange
            const string productCode = null;
            const int price = 5;
            const int quantity = 5;
            const string appliedCampaignName = "opportunity";

            //Act 
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => Order.Create(productCode, price, appliedCampaignName, quantity));

            //Assert
            Assert.Equal(MessageConstants.NullProductCodeError, actualException.Message);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void OrderDomainCreate_WithInvalidQuantity_ThrowsException(int quantity)
        {
            //Arrange
            const string productCode = "A1234";
            const int price = 5;
            const string appliedCampaignName = "opportunity";

            //Act
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => Order.Create(productCode, price, appliedCampaignName, quantity));


            //Assert
            Assert.Equal(MessageConstants.QuantityValueLimitError, actualException.Message);
        }


        [Theory]
        [InlineData(-1)]
        public void OrderDomainCreate_WithInValidPrice_ThrowsException(double price)
        {
            //Arrange
            const string productCode = "A1234";
            const int quantity = 11;
            string appliedCampaignName = "opportunity";

            //Act
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => Order.Create(productCode, price, appliedCampaignName, quantity));


            //Assert
            Assert.Equal( MessageConstants.PromotedPriceError, actualException.Message);
        }


        #endregion

        #region OrderedProduct

        [Fact]
        public void OrderedProductCreate_WithGivenCorrectValues_CreatesCorrectOrder()
        {
            //Arrange
            const string productCode = "A1234";
            const int price = 5;
            const string appliedCampaignName = "opportunity";

            //Act
            var orderedProduct = OrderedProduct.Create(productCode, price, appliedCampaignName);

            //Assert
            Assert.Equal(productCode, orderedProduct.ProductCode);
            Assert.Equal(price, orderedProduct.Price);
            Assert.Equal(appliedCampaignName, orderedProduct.AppliedCampaign);
        }

        [Fact]
        public void OrderedProductCreate_WithGivenSameValues_CreatesUniqueDifferentEntities()
        {
            //Arrange
            const string productCode = "A1234";
            const int price = 5;
            const string appliedCampaignName = "opportunity";

            //Act
            var firstOrderedProduct = OrderedProduct.Create(productCode, price, appliedCampaignName);
            var secondOrderedProduct = OrderedProduct.Create(productCode, price, appliedCampaignName);

            //Assert
            Assert.NotEqual(firstOrderedProduct.Id, secondOrderedProduct.Id);
        }

        [Fact]
        public void OrderedProductCreate_WithNullProductCode_ThrowsException()
        {
            //Arrange
            const string productCode = null;
            const int price = 5;
            const string appliedCampaignName = "opportunity";

            //Act 
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => OrderedProduct.Create(productCode, price, appliedCampaignName));

            //Assert
            Assert.Equal(MessageConstants.NullProductCodeError, actualException.Message);
        }

        [Theory]
        [InlineData(-1)]
        public void OrderedProductCreate_WithInvalidPrice_ThrowsException(int price)
        {
            //Arrange
            const string productCode = "A123";
            const string appliedCampaignName = "opportunity";

            //Act 
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => OrderedProduct.Create(productCode, price, appliedCampaignName));

            //Assert
            Assert.Equal(MessageConstants.PromotedPriceError, actualException.Message);
        }

        
        [Fact]
        public void OrderDomainCreate_WithGivenCorrectValues_CreatesCorrectOrder()
        {
            //Arrange
            const string productCode = "A1234";
            const int price = 5;
            const int quantity = 5;
            string appliedCampaignName = "opportunity";

            //Act
            var order = Order.Create(productCode, price, appliedCampaignName, quantity);

            //Assert
            Assert.Equal(productCode, order.Product.ProductCode);
            Assert.Equal(price, order.Product.Price);
            Assert.Equal(appliedCampaignName, order.Product.AppliedCampaign);
            Assert.Equal(quantity, order.Quantity);
        }

        #endregion
    }
}
