
using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.Promotions.PromotionStatuses
{
   public interface IPromotionStatus
    {
        double ApplyPromotion(double price, float priceManipulationLimit);
        PromotionType GetStatus();


    }
}
