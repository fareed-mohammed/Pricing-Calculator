namespace PricingCalculator.Discounts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Enums;
    using Helpers;

    public class HalfPriceDiscount : IDiscount
    {
        private readonly ProductQuantity _productsThatQualifyBasketforDiscount;
        private readonly DiscountedProduct _discountProduct;

        public HalfPriceDiscount(ProductQuantity productsThatQualifyBasketforDiscount, DiscountedProduct discountProduct)
        {            
            _productsThatQualifyBasketforDiscount = productsThatQualifyBasketforDiscount ?? throw new ArgumentNullException(nameof(productsThatQualifyBasketforDiscount));
            _discountProduct = discountProduct ?? throw new ArgumentNullException(nameof(discountProduct));
        }
        private decimal ApplyDiscount(Product item) => Math.Round(item.UnitPrice * 0.5m, 2);

        public IEnumerable<AppliedDiscount> DiscountsApplicable(IEnumerable<ProductQuantity> items)
        {
            var itemsArray = items as ProductQuantity[] ?? items.ToArray();

            var discountsApplied = new List<AppliedDiscount>();

            foreach (var item in itemsArray)
            {
                if (item.Product.ProductId == _productsThatQualifyBasketforDiscount.Product.ProductId && item.Quantity >= _productsThatQualifyBasketforDiscount.Quantity)
                {
                    var halfPriceItems = itemsArray
                        .Where(halfPriceItem => halfPriceItem.Product.ProductId == _discountProduct.ProductId)
                        .ToArray();

                    if (halfPriceItems.Length > 0)
                    {
                        var discount = ApplyDiscount(halfPriceItems[0].Product);

                        discountsApplied.Add(new AppliedDiscount
                        {
                            Type = DiscountType.HalfPrice,
                            Text = $"{_discountProduct.Name} 50% OFF: - {discount.ToCurrencyString()}",
                            Amount = discount
                        });
                    }
                }
            }

            return discountsApplied;
        }
    }
}
