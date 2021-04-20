using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.Promotions.PromotionStatuses
{ 
    //GOF STATE PATTERN.
   public class PromotionStatus : IPromotionStatus
    {
        private IPromotionStatus _promotionStatus;
        private PromotionType _promotionType;
        public PromotionStatus()
        {
            _promotionStatus = new OpportunityPromotion();
        }
        public double ApplyPromotion(double price, float priceManipulationLimit)
        {
            return _promotionStatus.ApplyPromotion( price,  priceManipulationLimit);
        }

        public void ChangeStatus(IPromotionStatus promotionStatus)
        {
            _promotionStatus = promotionStatus;
          
        }

        public PromotionType GetStatus()
        {
            return _promotionType;
        }
    }
}
