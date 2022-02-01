using Ecommence.Domain.Campaigns;
using Ecommence.Domain.SeedWork;
using Moq;
using Moq.Protected;
using Xunit;

namespace HepsiCampaign.Domain.Tests
{
    public class CampaignTest
    {
        [Fact]
        public void CampaignDomainCreate_GivenCorrectValues_CreatesCorrectOrder()
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;


            //Act
            var campaign = Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount);

            //Assert
            Assert.Equal(campaign.Name, name);
            Assert.Equal(campaign.Duration, duration);
            Assert.Equal(campaign.ProductCode, productCode);
            Assert.Equal(campaign.PriceManipulationLimit, priceManipulationLimit);
            Assert.Equal(campaign.TargetSalesCount, targetSalesCount);
        }


        [Fact]
        public void CampaignDomainCreate_GivenCorrectValues_CreatesUniqueDifferentEntities()
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;

            //Act
            var firstCampaign = Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount);
            var secondCampaign = Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount);
            //Assert
            Assert.NotEqual(firstCampaign.Id, secondCampaign.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(110)]
        public void CampaignDomainCreate_WithWrongPriceManipulationValue_ThrowsError(float priceManipulationLimit)
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const int targetSalesCount = 100;

            //Act 
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount));


            //Assert
            Assert.Equal(exception.Message, MessageConstants.PriceManipulationLimitError);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void CampaignDomainCreate_WithWrongTargetSalesValue_ThrowsError(int targetSalesCount)
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;

            //Act 
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount));


            //Assert
            Assert.Equal(exception.Message, MessageConstants.TargetSalesCountError);
        }

        [Fact]
        public void CampaignDomainCreate_WithNegativeDurationValue_ThrowsError()
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = -10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;

            //Act 
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount));


            //Assert
            Assert.Equal(exception.Message, MessageConstants.DurationLimitError);
        }

        [Fact]
        public void CampaignDomainCreate_WithNullProductCode_ThrowsError()
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = null;
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;

            //Act 
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount));


            //Assert
            Assert.Equal(exception.Message, MessageConstants.NullProductCodeError);
        }

        [Fact]
        public void CampaignDomainCreate_WithNullName_ThrowsError()
        {
            //Arrange
            const string name = null;
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;

            //Act 
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount));


            //Assert
            Assert.Equal(exception.Message, MessageConstants.NullProductCodeError);
        }

        [Fact]
        public void SetAveragePriceValue_WithCorrectValues_CorrectResult()
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var campaign = Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount);
            const double averagePriceValue = 105.2;

            //Act
            campaign.SetAveragePriceValue(averagePriceValue);

           // var xxx = GetCampaign();
           // MockSalesAreSoGood(xxx,true);
           //var x = xxx.Object.SalesAreSoGood();

            //Assert
            Assert.Equal(campaign.AveragePriceValue, averagePriceValue);

        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void SetAveragePriceValue_WithWrongValues_ThrowsException(double averagePriceValue)
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var campaign = Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount);
   
            //Act
            var exception = Assert.Throws<BusinessRuleValidationException>(() => campaign.SetAveragePriceValue(averagePriceValue));  
        

            //Assert
            Assert.Equal(exception.Message, MessageConstants.NegativeOrZeroPriceError);

        }
        

        [Fact]
        public void IncreaseCampaignSalesCountByQuantity_WithTwoIncrement_IncreasesSalesCountCorrectly()
        {
            //Arrange
           const int quantity = 5;
           const int expectedValue = 10;

            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var campaign = Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount);
     
            //Act
             campaign.IncreaseCampaignSalesCountByQuantity(quantity);
             campaign.IncreaseCampaignSalesCountByQuantity(quantity);


            //Assert
            Assert.Equal(expectedValue,campaign.CampaignSalesCount);
        }

        [Fact]
        public void ObserveCampaignStatus_WithGoodSalesCount_ReturnsSmallCampaign()
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var campaign = Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount);
            campaign.IncreaseCampaignSalesCountByQuantity(70);

            //Act
            var actualResult = campaign.ObserveCampaignStatus();


            //Assert
            Assert.Equal(CampaignType.SmallCampaign, actualResult);
        }

        [Fact]
        public void ObserveCampaignStatus_WithBadSalesCount_ReturnsOpportunityCampaign()
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var campaign = Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount);
            campaign.IncreaseCampaignSalesCountByQuantity(10);

            //Act
            var actualResult = campaign.ObserveCampaignStatus();


            //Assert
            Assert.Equal(CampaignType.OpportunityCampaign, actualResult);
        }

        [Fact]
        public void ObserveCampaignStatus_WithNotBadSalesCount_ReturnsOpportunityCampaign()
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var campaign = Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount);
            campaign.IncreaseCampaignSalesCountByQuantity(40);
           
            //Act
            var actualResult = campaign.ObserveCampaignStatus();


            //Assert
            Assert.Equal(CampaignType.MediumCampaign, actualResult);
        }

        [Fact]
        public void CampaignDomainStatus_GivenExceedingTime_MakesPassive()
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 100;
            var campaign = Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount);

            //Act
            Date.Hour += 11;

            //Assert
            Assert.False(campaign.Status);
        }

        [Fact]
        public void CampaignDomainStatus_GivenExceedingCampaignSalesCountToTarget_MakesPassive()
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 10;
            var campaign = Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount);
            campaign.IncreaseCampaignSalesCountByQuantity(11);

            //Assert
            Assert.False(campaign.Status);
        }

        [Fact]
        public void CampaignDomainTurnOver_GivenValidValues_CalculatesTurnOver()
        {
            //Arrange
            const string name = "sweetNovember";
            const string productCode = "a123";
            const int duration = 10;
            const float priceManipulationLimit = 20;
            const int targetSalesCount = 10;
            var campaign = Campaign.Create(name, duration, productCode, priceManipulationLimit, targetSalesCount);
            campaign.IncreaseCampaignSalesCountByQuantity(11);
            campaign.SetAveragePriceValue(5);


            //Assert
            Assert.Equal(55, campaign.TurnOver);
        }

    }
}
