namespace TicketTypePromotion.Application.PromotionServices
{
    public class PromotionDto
    {
        public string Name { get; set; }
        public string TicketTypeCode { get; set; }
        public int Duration { get; set; }
        public float PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
        public double AveragePriceValue { get; set; }
        public bool Status { get; set; }
        public double TurnOver { get; set; }
        public int PromotionSalesCount { get; set; }
    }
}
