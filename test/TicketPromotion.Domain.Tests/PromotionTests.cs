using TicketTypePromotion.Domain.Promotions;
using TicketTypePromotion.Domain.SeedWork;
using Xunit;

namespace HepsiPromotion.Domain.Tests
{
    public class PromotionTest
    {
        [Fact]
        public void PromotionDomainCreate_GivenCorrectValues_CreatesCorrectReservation()
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;


            //Act
            var promotion = Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount);

            //Assert
            Assert.Equal(promotion.Name, name);
            Assert.Equal(promotion.Duration, duration);
            Assert.Equal(promotion.TicketTypeCode, ticketCode);
            Assert.Equal(promotion.PriceManipulationLimit, priceManipulationLimit);
            Assert.Equal(promotion.TargetSalesCount, targetSalesCount);
        }


        [Fact]
        public void PromotionDomainCreate_GivenCorrectValues_CreatesUniqueDifferentEntities()
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;

            //Act
            var firstPromotion = Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount);
            var secondPromotion = Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount);
            //Assert
            Assert.NotEqual(firstPromotion.Id, secondPromotion.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(110)]
        public void PromotionDomainCreate_WithWrongPriceManipulationValue_ThrowsError(float priceManipulationLimit)
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const int targetSalesCount = 100;

            //Act 
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount));


            //Assert
            Assert.Equal(exception.Message, MessageConstants.PriceManipulationLimitError);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void PromotionDomainCreate_WithWrongTargetSalesValue_ThrowsError(int targetSalesCount)
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;

            //Act 
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount));


            //Assert
            Assert.Equal(exception.Message, MessageConstants.TargetSalesCountError);
        }

        [Fact]
        public void PromotionDomainCreate_WithNegativeDurationValue_ThrowsError()
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = -10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;

            //Act 
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount));


            //Assert
            Assert.Equal(exception.Message, MessageConstants.DurationLimitError);
        }

        [Fact]
        public void PromotionDomainCreate_WithNullTicketTypeCode_ThrowsError()
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = null;
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;

            //Act 
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount));


            //Assert
            Assert.Equal(exception.Message, MessageConstants.NullTicketTypeCodeError);
        }

        [Fact]
        public void PromotionDomainCreate_WithNullName_ThrowsError()
        {
            //Arrange
            const string name = null;
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;

            //Act 
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount));


            //Assert
            Assert.Equal(exception.Message, MessageConstants.NullTicketTypeCodeError);
        }

        [Fact]
        public void SetAveragePriceValue_WithCorrectValues_CorrectResult()
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var promotion = Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount);
            const double averagePriceValue = 105.2;

            //Act
            promotion.SetAveragePriceValue(averagePriceValue);


            //Assert
            Assert.Equal(promotion.AveragePriceValue, averagePriceValue);

        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void SetAveragePriceValue_WithWrongValues_ThrowsException(double averagePriceValue)
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var promotion = Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount);
   
            //Act
            var exception = Assert.Throws<BusinessRuleValidationException>(() => promotion.SetAveragePriceValue(averagePriceValue));  
        

            //Assert
            Assert.Equal(exception.Message, MessageConstants.NegativeOrZeroPriceError);

        }
        

        [Fact]
        public void IncreasePromotionSalesCountByQuantity_WithTwoIncrement_IncreasesSalesCountCorrectly()
        {
            //Arrange
           const int quantity = 5;
           const int expectedValue = 10;

            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var promotion = Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount);
     
            //Act
             promotion.IncreasePromotionSalesCountByQuantity(quantity);
             promotion.IncreasePromotionSalesCountByQuantity(quantity);


            //Assert
            Assert.Equal(expectedValue,promotion.PromotionSalesCount);
        }

        [Fact]
        public void ObservePromotionStatus_WithGoodSalesCount_ReturnsSmallPromotion()
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var promotion = Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount);
            promotion.IncreasePromotionSalesCountByQuantity(70);

            //Act
            var actualResult = promotion.ObservePromotionStatus();


            //Assert
            Assert.Equal(PromotionType.SmallPromotion, actualResult);
        }

        [Fact]
        public void ObservePromotionStatus_WithBadSalesCount_ReturnsOpportunityPromotion()
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var promotion = Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount);
            promotion.IncreasePromotionSalesCountByQuantity(10);

            //Act
            var actualResult = promotion.ObservePromotionStatus();


            //Assert
            Assert.Equal(PromotionType.OpportunityPromotion, actualResult);
        }

        [Fact]
        public void ObservePromotionStatus_WithNotBadSalesCount_ReturnsOpportunityPromotion()
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var promotion = Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount);
            promotion.IncreasePromotionSalesCountByQuantity(40);
           
            //Act
            var actualResult = promotion.ObservePromotionStatus();


            //Assert
            Assert.Equal(PromotionType.MediumPromotion, actualResult);
        }

        [Fact]
        public void PromotionDomainStatus_GivenExceedingTime_MakesPassive()
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var promotion = Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount);

            //Act
            Date.Hour += 11;

            //Assert
            Assert.False(promotion.Status);
        }

        [Fact]
        public void PromotionDomainStatus_GivenExceedingPromotionSalesCountToTarget_MakesPassive()
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 10;
            var promotion = Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount);
            promotion.IncreasePromotionSalesCountByQuantity(11);

            //Assert
            Assert.False(promotion.Status);
        }

        [Fact]
        public void PromotionDomainTurnOver_GivenValidValues_CalculatesTurnOver()
        {
            //Arrange
            const string name = "sweetNovember";
            const string ticketCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 10;
            var promotion = Promotion.Create(name, duration, ticketCode, priceManipulationLimit, targetSalesCount);
            promotion.IncreasePromotionSalesCountByQuantity(11);
            promotion.SetAveragePriceValue(5);


            //Assert
            Assert.Equal(55, promotion.TurnOver);
        }

    }
}
