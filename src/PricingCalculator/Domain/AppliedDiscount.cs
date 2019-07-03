namespace PricingCalculator.Domain
{
    using Enums;

    public class AppliedDiscount
    {
        public DiscountType Type { get; set; }
        public string Text { get; set; }
        public decimal Amount { get; set; }        
    }
}
