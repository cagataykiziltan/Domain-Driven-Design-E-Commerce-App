using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.Promotions.PromotionStatuses
{
    public class MediumPromotion : IPromotionStatus
    {
        //two thirds of price manipulation limit is applied
        public double ApplyPromotion(double price,float priceManipulationLimit)
        {
            return price - (price * ((priceManipulationLimit / 100) / 1.5));
        }

        public PromotionType GetStatus()
        {
            return PromotionType.MediumPromotion;
        }

        //COMES OTHER LOGICS WHEN MEDIUM CAMPAIGN ENABLED
    }
}
