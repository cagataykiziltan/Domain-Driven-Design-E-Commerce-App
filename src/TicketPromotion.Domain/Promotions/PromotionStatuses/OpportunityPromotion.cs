using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.Promotions.PromotionStatuses
{
    public class OpportunityPromotion : IPromotionStatus
    {
        //max price manipulation limit is applied
        public double ApplyPromotion(double price,float priceManipulationLimit)
        {
            return price - (price * (priceManipulationLimit/100));
        }

        public PromotionType GetStatus()
        {
            return PromotionType.OpportunityPromotion;
        }

        //OTHER LOGICS COMES WHEN OPPORTUNITY CAMPAIGN ENABLED
    }
}
