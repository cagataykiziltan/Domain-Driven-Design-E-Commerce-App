using Ecommence.Application.CampaignServices;
using System.Threading.Tasks;

namespace Ecommence.Application.Common.Interfaces
{
    public interface ICampaignService
    {
        Task<CampaignDto> CreateCampaign(CampaignDto campaign);

        Task<CampaignDto> GetCampaignByName(string campaignName);
    }
}
