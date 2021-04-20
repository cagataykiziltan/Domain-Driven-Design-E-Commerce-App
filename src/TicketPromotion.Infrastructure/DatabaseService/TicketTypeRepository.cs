using TicketTypePromotion.Domain.TicketTypes;
using System.Collections.Generic;
using System.Linq;
using TicketTypePromotion.Application.Common.Interfaces;

namespace TicketTypePromotion.Infrastructure.DatabaseService
{
    public class TicketTypeRepository : ITicketTypeRepository
    {
        private readonly List<TicketType> _tickets;
        public TicketTypeRepository()
        {
            _tickets = new List<TicketType>();
        }
        public void Create(TicketType entity) => _tickets.Add(entity);
   
        public TicketType GetByTicketTypeCode(string ticketCode) => _tickets.FirstOrDefault(x => x.TicketTypeCode == ticketCode);
        
    }
}
