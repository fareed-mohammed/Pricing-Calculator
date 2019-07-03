namespace PricingCalculator.Helpers
{
    using System.Collections.Generic;
    using Discounts;
    using Domain;
    using System;
    using System.Linq;
    using Factories;
    using PricingCalculator.Builders;

    public static class Utilities
    {
        public static string ToCurrencyString(this decimal value)
        {
            return value < 1 ? $"{(int) (value * 100)}p" : $"{value:C}";            
        }

        // This discounts list would be read from a configuration file or database as not to change the code 
        // everytime a discount was added/amended/removed
        public static IEnumerable<IDiscount> CreateDiscounts()
        {
            var productBuilder = new ProductBuilder();

            return new List<IDiscount>
            {
                new PercentageDiscount(
                        new DiscountedProduct
                        {
                            ProductId = 1,
                            Name = "Apples"
                        }
                    ,
                    0.10m),

                new HalfPriceDiscount(
                    new ProductQuantity
                    {
                        Product = productBuilder.Create("Beans")
                                            .WithProductId(2),
                        Quantity = 2

                    },
                    new DiscountedProduct()
                    {
                        ProductId = 3,
                        Name = "Bread"
                    })
            };
        }

        public static IEnumerable<ProductQuantity> CreateProducts(IEnumerable<string> products)
        {
            var productsAndQuantities = new List<ProductQuantity>();

            var productFactory = new ProductFactory();

            foreach (var product in products)
            {
                var existProduct = productsAndQuantities.SingleOrDefault(item =>
                    string.Equals(item.Product.Name, product, StringComparison.CurrentCultureIgnoreCase));

                if (existProduct == null)
                {
                    productsAndQuantities.Add(new ProductQuantity
                    {
                        Product = productFactory.CreateProduct(product),
                        Quantity = 1
                    });
                }

                if (existProduct != null)
                    existProduct.Quantity += 1;
            }

            return productsAndQuantities;            
        }
    }
}
