namespace PricingCalculator.Discounts
{
    using System.Collections.Generic;
    using Domain;

    public interface IDiscount
    {
        IEnumerable<AppliedDiscount> DiscountsApplicable(IEnumerable<ProductQuantity> items);
    }
}
