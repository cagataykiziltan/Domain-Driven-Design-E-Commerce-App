using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Campaigns.CampaignStatuses
{
    public class MediumCampaign : ICampaignStatus
    {
        //two thirds of price manipulation limit is applied
        public double ApplyCampaign(double price,float priceManipulationLimit)
        {
            return price - (price * ((priceManipulationLimit / 100) / 1.5));
        }

        public CampaignType GetStatus()
        {
            return CampaignType.MediumCampaign;
        }

        //COMES OTHER LOGICS WHEN MEDIUM CAMPAIGN ENABLED
    }
}
