using TicketTypePromotion.Application.PromotionServices;

namespace TicketTypePromotion.Application.Common.Interfaces
{
    public interface IPromotionService
    {
        PromotionDto CreatePromotion(PromotionDto promotion);
   
        PromotionDto GetPromotionByName(string promotionName);
    }
}
