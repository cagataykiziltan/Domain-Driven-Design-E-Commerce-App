namespace TicketTypePromotion.Application.TicketTypeServices
{
    public class TicketTypeDto
    {
        public string TicketTypeCode { get; set; }
        public double Price { get; set; }
        public double PromotedPrice { get; set; }
        public int Stock { get; set; }
    }
}
