using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Campaigns.CampaignStatuses
{ 
    //GOF STATE PATTERN.
   public class CampaignStatus : ICampaignStatus
    {
        private ICampaignStatus _campaignStatus;
        private CampaignType _campaignType;
        public CampaignStatus()
        {
            _campaignStatus = new OpportunityCampaign();
        }
        public double ApplyCampaign(double price, float priceManipulationLimit)
        {
            return _campaignStatus.ApplyCampaign( price,  priceManipulationLimit);
        }

        public void ChangeStatus(ICampaignStatus campaignStatus)
        {
            _campaignStatus = campaignStatus;
          
        }

        public CampaignType GetStatus()
        {
            return _campaignType;
        }
    }
}
