namespace Ecommence.Domain.SeedWork
{
    public static class MessageConstants
    {
        public static string PriceManipulationLimitError = "Price Manipulation Limit Must Be Between 0 and 100";
        public static string QuantityValueLimitError = "Quantity can not be negative or zero";
        public static string NegativeOrZeroPriceError = "Price can not be zero or negative  ";
        public static string NullProductCodeError = "Product code can not be null";
        public static string NegativeOrZeroStockError = "Stock can not be negative";
        public static string TargetSalesCountError = "Target sales count can not be negative";
        public static string DurationLimitError = "Duration can not be negative";


        public static string NullParameterError = "Null Parameter!";
        public static string ProductNotFoundError = "Product not found";
        public static string StockQuantityError = "Please check stock and quantity";
        public static string DuplicateProductError = "There is already a product with same product code";
        public static string ExistingCampaignError = "There is already a campaign with same name for same product";
        public static string CampaignNotFoundError = "Campaign not found";
        public static string PromotedPriceError = "Promoted price can not be negative";


        
    }
}
