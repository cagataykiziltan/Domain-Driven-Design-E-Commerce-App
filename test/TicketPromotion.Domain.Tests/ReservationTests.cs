using TicketTypePromotion.Domain.Reservations;
using TicketTypePromotion.Domain.SeedWork;
using Xunit;

namespace HepsiPromotion.Domain.Tests
{
    public class ReservationTests
    {
        #region Reservation

        [Fact]
        public void ReservationDomainCreate_WithGivenCorrectValues_CreatesCorrectReservation()
        {
            //Arrange
            const string ticketCode = "A1234";
            const int price = 5;
            const int quantity = 5;
            string appliedPromotionName = "opportunity";

            //Act
            var order = Reservation.Create(ticketCode, price, appliedPromotionName, quantity);

            //Assert
            Assert.Equal(ticketCode, order.TicketType.TicketTypeCode);
            Assert.Equal(price, order.TicketType.Price);
            Assert.Equal(appliedPromotionName, order.TicketType.AppliedPromotion);
            Assert.Equal(quantity, order.Quantity);
        }

        [Fact]
        public void ReservationDomainCreate_WithGivenWithSameValues_CreatesUniqueDifferentEntities()
        {
            //Arrange
            const string ticketCode = "A1234";
            const int price = 5;
            const int quantity = 5;
            const string appliedPromotionName = "opportunity";

            //Act
            var firstReservation = Reservation.Create(ticketCode, price, appliedPromotionName, quantity);
            var secondReservation = Reservation.Create(ticketCode, price, appliedPromotionName, quantity);

            //Assert
            Assert.NotEqual(firstReservation.Id, secondReservation.Id);
        }

        [Fact]
        public void ReservationDomainCreate_WithNullTicketTypeCode_ThrowsException()
        {
            //Arrange
            const string ticketCode = null;
            const int price = 5;
            const int quantity = 5;
            const string appliedPromotionName = "opportunity";

            //Act 
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => Reservation.Create(ticketCode, price, appliedPromotionName, quantity));

            //Assert
            Assert.Equal(MessageConstants.NullTicketTypeCodeError, actualException.Message);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void ReservationDomainCreate_WithInvalidQuantity_ThrowsException(int quantity)
        {
            //Arrange
            const string ticketCode = "A1234";
            const int price = 5;
            const string appliedPromotionName = "opportunity";

            //Act
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => Reservation.Create(ticketCode, price, appliedPromotionName, quantity));


            //Assert
            Assert.Equal(MessageConstants.QuantityValueLimitError, actualException.Message);
        }


        [Theory]
        [InlineData(-1)]
        public void ReservationDomainCreate_WithInValidPrice_ThrowsException(double price)
        {
            //Arrange
            const string ticketCode = "A1234";
            const int quantity = 11;
            string appliedPromotionName = "opportunity";

            //Act
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => Reservation.Create(ticketCode, price, appliedPromotionName, quantity));


            //Assert
            Assert.Equal( MessageConstants.PromotedPriceError, actualException.Message);
        }


        #endregion

        #region ReservationedTicket

        [Fact]
        public void ReservationedTicketCreate_WithGivenCorrectValues_CreatesCorrectReservation()
        {
            //Arrange
            const string ticketCode = "A1234";
            const int price = 5;
            const string appliedPromotionName = "opportunity";

            //Act
            var orderedTicket = ReservationedTicketType.Create(ticketCode, price, appliedPromotionName);

            //Assert
            Assert.Equal(ticketCode, orderedTicket.TicketTypeCode);
            Assert.Equal(price, orderedTicket.Price);
            Assert.Equal(appliedPromotionName, orderedTicket.AppliedPromotion);
        }

        [Fact]
        public void ReservationedTicketCreate_WithGivenSameValues_CreatesUniqueDifferentEntities()
        {
            //Arrange
            const string ticketCode = "A1234";
            const int price = 5;
            const string appliedPromotionName = "opportunity";

            //Act
            var firstReservationedTicket = ReservationedTicketType.Create(ticketCode, price, appliedPromotionName);
            var secondReservationedTicket = ReservationedTicketType.Create(ticketCode, price, appliedPromotionName);

            //Assert
            Assert.NotEqual(firstReservationedTicket.Id, secondReservationedTicket.Id);
        }

        [Fact]
        public void ReservationedTicketCreate_WithNullTicketTypeCode_ThrowsException()
        {
            //Arrange
            const string ticketCode = null;
            const int price = 5;
            const string appliedPromotionName = "opportunity";

            //Act 
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => ReservationedTicketType.Create(ticketCode, price, appliedPromotionName));

            //Assert
            Assert.Equal(MessageConstants.NullTicketTypeCodeError, actualException.Message);
        }

        [Theory]
        [InlineData(-1)]
        public void ReservationedTicketCreate_WithInvalidPrice_ThrowsException(int price)
        {
            //Arrange
            const string ticketCode = "A123";
            const string appliedPromotionName = "opportunity";

            //Act 
            var actualException = Assert.Throws<BusinessRuleValidationException>(() => ReservationedTicketType.Create(ticketCode, price, appliedPromotionName));

            //Assert
            Assert.Equal(MessageConstants.PromotedPriceError, actualException.Message);
        }


        #endregion
    }
}
