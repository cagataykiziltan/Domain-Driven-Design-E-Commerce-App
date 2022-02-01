using Ecommence.Domain.Campaigns;

namespace Ecommence.Application.Common.Interfaces
{
    public interface ICampaignRepository
    {
      void Create(Campaign entity);
      Campaign GetByName(string name);
      Campaign GetByProductCode(string productCode);
      bool CheckExistingCampaignByProductCodeAndName(string productCode, string name);
    }
}
