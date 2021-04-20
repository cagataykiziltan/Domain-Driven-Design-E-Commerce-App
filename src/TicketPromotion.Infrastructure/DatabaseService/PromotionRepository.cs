using TicketTypePromotion.Domain.Promotions;
using System.Collections.Generic;
using System.Linq;
using TicketTypePromotion.Application.Common.Interfaces;

namespace TicketTypePromotion.Infrastructure.DatabaseService
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly List<Promotion> _promotions;
        public PromotionRepository()
        {
            _promotions = new List<Promotion>();
        }
        public void Create(Promotion entity) => _promotions.Add(entity);
    
        public Promotion GetByName(string name) => _promotions.FirstOrDefault(x => x.Name == name);

        public Promotion GetByTicketTypeCode(string ticketCode) => _promotions.FirstOrDefault(x => x.TicketTypeCode == ticketCode && x.Status);

        public bool CheckExistingPromotionByTicketTypeCodeAndName(string ticketCode,string name) => _promotions.Exists(x => (x.TicketTypeCode == ticketCode || x.Name == name) &&
                                                                                                                                     x.Status);
    }
}
