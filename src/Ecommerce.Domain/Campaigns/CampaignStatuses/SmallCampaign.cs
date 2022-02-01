using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Campaigns.CampaignStatuses
{
    public class SmallCampaign : ICampaignStatus
    {
        //One third of price manipulation limit is applied
        public double ApplyCampaign(double price, float priceManipulationLimit)
        {
            return price - (price * (priceManipulationLimit / 100) / 3);
        }

        public CampaignType GetStatus()
        {
            return CampaignType.SmallCampaign;
        }

        //COMES OTHER LOGICS WHEN SMALL CAMPAIGN ENABLED
    }
}
