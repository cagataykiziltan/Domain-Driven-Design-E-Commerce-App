using TicketTypePromotion.Domain.Promotions;

namespace TicketTypePromotion.Application.Common.Interfaces
{
    public interface IPromotionRepository
    {
      void Create(Promotion entity);
      Promotion GetByName(string name);
      Promotion GetByTicketTypeCode(string ticketCode);
      bool CheckExistingPromotionByTicketTypeCodeAndName(string ticketCode, string name);
    }
}
