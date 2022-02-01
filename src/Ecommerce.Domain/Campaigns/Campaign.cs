using System;
using Ecommence.Domain.Campaigns.CampaignStatuses;
using Ecommence.Domain.Campaigns.Rules;
using Ecommence.Domain.Products.Rules;
using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Campaigns
{
    public class Campaign : EntityObject<Guid>
    {
        public string Name { get; private set; }
        public string ProductCode { get; private set; }
        public int Duration { get; private set; }
        public float PriceManipulationLimit { get; private set; }
        public int TargetSalesCount { get; private set; }
        private CampaignStatus CampaignStatus { get; set; }
        public double AveragePriceValue { get; private set; }
        public int CampaignSalesCount { get; private set; }
        public double TurnOver => (AveragePriceValue * CampaignSalesCount);
        public bool Status => (Duration > Date.Hour && CampaignSalesCount < TargetSalesCount);

        public static Campaign Create(Guid id, string name, int duration, string productCode, float priceManipulationLimit, int targetSalesCount)
        {
            CheckRule(new PriceManipulationLimitMustBeBetween0And100(priceManipulationLimit));
            CheckRule(new ProductCodeCannotBeNull(productCode));
            CheckRule(new ProductCodeCannotBeNull(name));
            CheckRule(new TargetSalesCountCannotBeNegativeOrZero(targetSalesCount));
            CheckRule(new DurationCannotBeNegative(duration));

            var campaign = new Campaign
            {
                Id = id,
                Name = name,
                Duration = duration,
                PriceManipulationLimit = priceManipulationLimit,
                TargetSalesCount = targetSalesCount,
                ProductCode = productCode,
                CampaignStatus = new CampaignStatus()
            };

            return campaign;
        }

        public void SetAveragePriceValue(double averagePrice)
        {
            CheckRule(new Orders.Rules.PriceCannotBeZeroOrNegative(averagePrice));

            AveragePriceValue = averagePrice;
        }

        public double ApplyCampaignToPrice(double price)
        {
            var promotedPrice = CampaignStatus.ApplyCampaign(price, PriceManipulationLimit);

            return promotedPrice;
        }

        public void IncreaseCampaignSalesCountByQuantity(int quantity)
        {
            CampaignSalesCount += quantity;
            ObserveCampaignStatus();
        }

        public CampaignType ObserveCampaignStatus()
        {

            if (SalesAreSoGood())
            {
                CampaignStatus.ChangeStatus(new SmallCampaign());
                return CampaignType.SmallCampaign;
            }

            if (SalesAreNotBad())
            {
                CampaignStatus.ChangeStatus(new MediumCampaign());
                return CampaignType.MediumCampaign;
            }

            if (SalesAreGoingBad())
            {
                CampaignStatus.ChangeStatus(new OpportunityCampaign());
                return CampaignType.OpportunityCampaign;
            }

            return CampaignType.NoCampaign;
        }

        private bool SalesAreSoGood() => TargetSalesCount * 0.5 <= CampaignSalesCount && CampaignSalesCount <= TargetSalesCount;

        private bool SalesAreNotBad() => TargetSalesCount * 0.2 <= CampaignSalesCount && CampaignSalesCount < TargetSalesCount * 0.5;

        private bool SalesAreGoingBad() => CampaignSalesCount < TargetSalesCount * 0.2;


    }

}

