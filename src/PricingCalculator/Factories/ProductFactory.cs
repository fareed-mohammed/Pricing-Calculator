namespace PricingCalculator.Factories
{
    using System;
    using Domain;

    public class ProductFactory
    {
        // This product list would be read from a configuration file or database as not to change the code 
        // everytime a product was added/amended/removed
        public Product CreateProduct(string productName)
        {
            switch (productName.ToLower())
            {
                case "apples":
                    return new Product
                    {
                        ProductId = 1,
                        Name = "Apples",
                        UnitPrice = 1.00m
                    };
                case "beans":
                    return new Product
                    {
                        ProductId = 2,
                        Name = "Beans",
                        UnitPrice = 0.65m
                    };
                case "bread":
                    return new Product
                    {
                        ProductId = 3,
                        Name = "Bread",
                        UnitPrice = 0.80m
                    };
                case "milk":
                    return new Product
                    {
                        ProductId = 4,
                        Name = "Milk",
                        UnitPrice = 1.30m
                    };
                default:
                    throw new NotSupportedException($"Unrecognized product name : {productName.ToLower()}");
            }
        }
    }
}
