namespace PricingCalculator
{
    using System.Collections.Generic;
    using Domain;

    public interface IShoppingBasket
    {
        void AddProducts(IEnumerable<ProductQuantity> items);
        int ProductCount { get; }
        decimal SubTotal { get; }
        IEnumerable<AppliedDiscount> GetBasketDiscounts();
    }
}
