namespace Ecommence.Application.CampaignServices
{
    public class CampaignDto
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public float PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
        public double AveragePriceValue { get; set; }
        public bool Status { get; set; }
        public double TurnOver { get; set; }
        public int CampaignSalesCount { get; set; }
    }
}
