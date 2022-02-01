using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Campaigns.CampaignStatuses
{
    public class OpportunityCampaign : ICampaignStatus
    {
        //max price manipulation limit is applied
        public double ApplyCampaign(double price,float priceManipulationLimit)
        {
            return price - (price * (priceManipulationLimit/100));
        }

        public CampaignType GetStatus()
        {
            return CampaignType.OpportunityCampaign;
        }

        //OTHER LOGICS COMES WHEN OPPORTUNITY CAMPAIGN ENABLED
    }
}
