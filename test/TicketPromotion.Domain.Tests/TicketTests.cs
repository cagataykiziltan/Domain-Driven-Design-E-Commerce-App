using TicketTypePromotion.Domain.TicketTypes;
using TicketTypePromotion.Domain.SeedWork;
using Xunit;

namespace HepsiPromotion.Domain.Tests
{
    public class TicketTests
    {
        [Fact]
        public void TicketDomainCreate_WithGivenCorrectValues_CreatesCorrectTicket()
        {
            //Arrange
            const string ticketCode = "A1234";
            const int price = 5;
            const int stock = 5;
            
            //Act
            var ticketType = TicketType.Create(ticketCode, price, stock);

            //Assert
            Assert.Equal(ticketCode, ticketType.TicketTypeCode);
            Assert.Equal(price, ticketType.Price);
            Assert.Equal(stock, ticketType.Stock);
        }

        [Fact]
        public void TicketDomainCreate_WithGivenSameValues_CreatesUniqueDifferentEntities()
        {
            //Arrange
            const string ticketCode = "A1234";
            const int price = 5;
            const int stock = 5;

            //Act
            var firstTicket = TicketType.Create(ticketCode, price, stock);
            var secondTicket = TicketType.Create(ticketCode, price, stock);

            //Assert
            Assert.NotEqual(firstTicket.Id, secondTicket.Id);
        }

        [Fact]
        public void TicketDomainCreate_WithNullTicketTypeCode_ThrowsException()
        {
            //Arrange
            const string ticketCode = null;
            const int price = 5;
            const int stock = 5;

            //Act 
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => TicketType.Create(ticketCode, price, stock));

            //Assert
            Assert.Equal( MessageConstants.NullTicketTypeCodeError, actualException.Message);
        }

        [Theory]
        [InlineData(-1)]
        public void TicketDomainCreate_WithInvalidQuantity_ThrowsException(int stock)
        {
            //Arrange
            const string ticketCode = "A1234";
            const int price = 5;
      
            //Act 
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => TicketType.Create(ticketCode, price, stock));

            //Assert
            Assert.Equal(MessageConstants.NegativeOrZeroStockError, actualException.Message);
        }

        [Theory]
        [InlineData(-1)]
        public void TicketDomainCreate_WithInvalidPrice_ThrowsException(int price)
        {
            //Arrange
            const string ticketCode = "A1234";
            const int stock = 5;

            //Act 
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => TicketType.Create(ticketCode, price, stock));


            //Assert
            Assert.Equal(MessageConstants.NegativeOrZeroPriceError, actualException.Message);
        }

   
        [Fact]
        public void SetPromotedPrice_WithCorrectValue_SetCorrectly()
        {
            //Arrange
            const string ticketCode = "A1234";
            const int price = 5;
            const int stock = 5;
            const int promotedPrice = 7;
            var ticketType = TicketType.Create(ticketCode, price, stock);

            //Act
            ticketType.SetPromotedPrice(promotedPrice);

            //Assert
            Assert.Equal(promotedPrice,ticketType.PromotedPrice);

        }

        [Theory]
        [InlineData(-1)]
        public void SetPromotedPrice_WithInvalidValue_ThrowsException(int promotedPrice)
        {
            //Arrange
            const string ticketCode = "A1234";
            const int price = 5;
            const int stock = 5;
            var ticket = TicketType.Create(ticketCode, price, stock);

            //Act
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => ticket.SetPromotedPrice(promotedPrice));
           

            //Assert
            Assert.Equal(MessageConstants.PromotedPriceError, actualException.Message);

        }
        
        
        [Fact]
        public void DecreaseStockQuantityByQuantity_WithCorrectValue_DecreasesCorrectly()
        {
            //Arrange
            const string ticketCode = "A1234";
            const int price = 5;
            const int stock = 5;
            const int quantity = 3;
            var ticketType = TicketType.Create(ticketCode, price, stock);
            const int expectedStock = 2;

            //Act
            ticketType.DecreaseStockQuantityByQuantity(quantity);


            //Assert
            Assert.Equal(expectedStock,ticketType.Stock);

        }
    }
}
