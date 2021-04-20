namespace TicketTypePromotion.Domain.SeedWork
{
    public static class MessageConstants
    {
        public static string PriceManipulationLimitError = "Price Manipulation Limit Must Be Between 0 and 100";
        public static string QuantityValueLimitError = "Quantity can not be negative or zero";
        public static string NegativeOrZeroPriceError = "Price can not be zero or negative  ";
        public static string NullTicketTypeCodeError = "TicketType code can not be null";
        public static string NegativeOrZeroStockError = "Stock can not be negative";
        public static string TargetSalesCountError = "Target sales count can not be negative";
        public static string DurationLimitError = "Duration can not be negative";


        public static string NullParameterError = "Null Parameter!";
        public static string TicketTypeNotFoundError = "TicketType not found";
        public static string StockQuantityError = "Please check stock and quantity";
        public static string DuplicateTicketTypeError = "There is already a ticketType with same ticketType code";
        public static string ExistingPromotionError = "There is already a promotion with same name for same ticketType";
        public static string PromotionNotFoundError = "Promotion not found";
        public static string PromotedPriceError = "Promoted price can not be negative";


        
    }
}
