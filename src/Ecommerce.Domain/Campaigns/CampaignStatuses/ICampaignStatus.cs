
using Ecommence.Domain.SeedWork;

namespace Ecommence.Domain.Campaigns.CampaignStatuses
{
   public interface ICampaignStatus
    {
        double ApplyCampaign(double price, float priceManipulationLimit);
        CampaignType GetStatus();


    }
}
