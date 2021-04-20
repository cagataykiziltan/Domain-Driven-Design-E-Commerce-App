using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.Promotions.PromotionStatuses
{
    public class SmallPromotion : IPromotionStatus
    {
        //One third of price manipulation limit is applied
        public double ApplyPromotion(double price, float priceManipulationLimit)
        {
            return price - (price * (priceManipulationLimit / 100) / 3);
        }

        public PromotionType GetStatus()
        {
            return PromotionType.SmallPromotion;
        }

        //COMES OTHER LOGICS WHEN SMALL CAMPAIGN ENABLED
    }
}
