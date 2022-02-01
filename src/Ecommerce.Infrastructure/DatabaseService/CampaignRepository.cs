using Ecommence.Domain.Campaigns;
using System.Collections.Generic;
using System.Linq;
using Ecommence.Application.Common.Interfaces;

namespace Ecommence.Infrastructure.DatabaseService
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly List<Campaign> _campaigns;
        public CampaignRepository()
        {
            _campaigns = new List<Campaign>();
        }
        public void Create(Campaign entity) => _campaigns.Add(entity);
    
        public Campaign GetByName(string name) => _campaigns.FirstOrDefault(x => x.Name == name);

        public Campaign GetByProductCode(string productCode) => _campaigns.FirstOrDefault(x => x.ProductCode == productCode && x.Status);

        public bool CheckExistingCampaignByProductCodeAndName(string productCode,string name) => _campaigns.Exists(x => (x.ProductCode == productCode || x.Name == name) &&
                                                                                                                                     x.Status);
    }
}
