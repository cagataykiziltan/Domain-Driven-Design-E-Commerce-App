using System;
using TicketTypePromotion.Domain.Promotions.PromotionStatuses;
using TicketTypePromotion.Domain.Promotions.Rules;
using TicketTypePromotion.Domain.TicketTypes.Rules;
using TicketTypePromotion.Domain.SeedWork;

namespace TicketTypePromotion.Domain.Promotions
{
    public class Promotion : EntityObject
    {
        public string Name { get; private set; }
        public string TicketTypeCode { get; private set; }
        public int Duration { get; private set; }
        public float PriceManipulationLimit { get; private set; }
        public int TargetSalesCount { get; private set; }
        private PromotionStatus PromotionStatus { get; set; }
        public double AveragePriceValue { get; private set; }
        public int PromotionSalesCount { get; private set; }
        public double TurnOver => (AveragePriceValue * PromotionSalesCount);
        public bool Status => (Duration > Date.Hour && PromotionSalesCount < TargetSalesCount);

        public static Promotion Create(string name, int duration, string ticketCode, float priceManipulationLimit, int targetSalesCount)
        {
            CheckRule(new PriceManipulationLimitMustBeBetween0And100(priceManipulationLimit));
            CheckRule(new TicketTypeCodeCannotBeNull(ticketCode));
            CheckRule(new TicketTypeCodeCannotBeNull(name));
            CheckRule(new TargetSalesCountCannotBeNegativeOrZero(targetSalesCount));
            CheckRule(new DurationCannotBeNegative(duration));

           var promotion = new Promotion
            {
                Id = Guid.NewGuid(),
                Name = name,
                Duration = duration,
                PriceManipulationLimit = priceManipulationLimit,
                TargetSalesCount = targetSalesCount,
                TicketTypeCode = ticketCode,
                PromotionStatus = new PromotionStatus()
           };

            return promotion;
        }

        public void SetAveragePriceValue(double averagePrice)
        {
            CheckRule(new Reservations.Rules.PriceCannotBeZeroOrNegative(averagePrice));

            AveragePriceValue = averagePrice;
        }

        public double ApplyPromotionToPrice(double price)
        {
            var promotedPrice = PromotionStatus.ApplyPromotion(price, PriceManipulationLimit);

            return promotedPrice;
        }
 
        public void IncreasePromotionSalesCountByQuantity(int quantity)
        {
            PromotionSalesCount += quantity;
            ObservePromotionStatus();
        }

        public PromotionType ObservePromotionStatus()
        {
         
            if (SalesAreSoGood())
            {
                PromotionStatus.ChangeStatus(new SmallPromotion());
                return PromotionType.SmallPromotion;
            }
            
            if (SalesAreNotBad())
            {
                PromotionStatus.ChangeStatus(new MediumPromotion());
                return PromotionType.MediumPromotion;
            }

            if (SalesAreGoingBad())
            {
                PromotionStatus.ChangeStatus(new OpportunityPromotion());
                return PromotionType.OpportunityPromotion;
            }

            return PromotionType.NoPromotion;
        }

        private  bool SalesAreSoGood() => TargetSalesCount * 0.5 <= PromotionSalesCount && PromotionSalesCount <= TargetSalesCount;

        private bool SalesAreNotBad() => TargetSalesCount * 0.2 <= PromotionSalesCount && PromotionSalesCount < TargetSalesCount * 0.5;

        private bool SalesAreGoingBad() => PromotionSalesCount < TargetSalesCount * 0.2;


    }
   
}

