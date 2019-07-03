namespace PricingCalculator.Discounts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Enums;
    using Helpers;

    public class PercentageDiscount : IDiscount
    {
        private readonly DiscountedProduct _discountedItem;
        private readonly decimal _percentage;

        public PercentageDiscount(DiscountedProduct discountedItem, decimal percentage)
        {
            _discountedItem = discountedItem ?? throw new ArgumentNullException(nameof(discountedItem));
            _percentage = percentage;
        }

        private decimal CalculateDiscount(ProductQuantity item) => Math.Round((item.Product.UnitPrice * item.Quantity) * _percentage, 2);

        public IEnumerable<AppliedDiscount> DiscountsApplicable(IEnumerable<ProductQuantity> items)
        {
            var itemArray = items as ProductQuantity[] ?? items.ToArray();

            var discountsApplied = new List<AppliedDiscount>();

            foreach (var item in itemArray)
            {               
                if (item.Product.ProductId == _discountedItem.ProductId)
                {
                    var discount = CalculateDiscount(item);
                    var appliedDiscount = new AppliedDiscount
                    {
                        Type = DiscountType.Percentage,
                        Text = $"{item.Product.Name} {_percentage:P0} OFF: - {discount.ToCurrencyString()}",
                        Amount = discount
                    };

                    discountsApplied.Add(appliedDiscount);
                }                
            }

            return discountsApplied;
        }
    }
}
