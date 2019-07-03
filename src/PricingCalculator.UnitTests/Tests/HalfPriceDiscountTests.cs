namespace PricingCalculator.UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PricingCalculator.Discounts;
    using PricingCalculator.Domain;
    using PricingCalculator.Enums;
    using PricingCalculator.UnitTests.Helpers;

    [TestClass]
    public class HalfPriceDiscountTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HalfPriceDiscount_ProductsThatQualifyBasketforDiscount_IsNull_ExceptionThrown()
        {
            // Arrange + Act + Assert
            var halfPriceDiscount = new HalfPriceDiscount(null, new DiscountedProduct());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HalfPriceDiscount_DiscountedProduct_IsNull_ExceptionThrown()
        {
            // Arrange + Act + Assert
            var halfPriceDiscount = new HalfPriceDiscount(new ProductQuantity(), null);
        }
        
        [TestMethod]
        public void HalfPriceDiscount_CalculateAppliedDiscount_WithQuantity_2OrMore()
        {
            // Arrange
            var percentageDiscount = new HalfPriceDiscount(new ProductQuantity
                                                            {
                                                                Product = new Product
                                                                {
                                                                    ProductId = 2,
                                                                    Name = "Beans"
                                                                },
                                                                Quantity = 2

                                                            },
                                                            new DiscountedProduct()
                                                            {
                                                                ProductId = 3,
                                                                Name = "Bread"
                                                            });

            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProducts()).ToArray();

            // Assert
            Assert.AreEqual(result.Any(), true);
            Assert.AreEqual(result[0].Type, DiscountType.HalfPrice);
            Assert.AreEqual(result[0].Amount, 0.40m);
            Assert.AreEqual(result[0].Text, "Bread 50% OFF: - 40p");
        }

        [TestMethod]
        public void HalfPriceDiscount_CalculateAppliedDiscount_WithQuantity_4OrMore()
        {
            // Arrange
            var percentageDiscount = new HalfPriceDiscount(new ProductQuantity
                {
                    Product = new Product
                    {
                        ProductId = 2,
                        Name = "Beans"
                    },
                    Quantity = 4

                },
                new DiscountedProduct()
                {
                    ProductId = 3,
                    Name = "Bread"
                });

            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProductsForHalfPriceDiscounts()).ToArray();

            // Assert
            Assert.AreEqual(result.Any(), true);
            Assert.AreEqual(result[0].Type, DiscountType.HalfPrice);
            Assert.AreEqual(result[0].Amount, 0.40m);
            Assert.AreEqual(result[0].Text, "Bread 50% OFF: - 40p");
        }

        [TestMethod]
        public void HalfPriceDiscount_NoDiscountApplied_WithQuantity_20rMore()
        {
            // Arrange
            var percentageDiscount = new HalfPriceDiscount(new ProductQuantity
                {
                    Product = new Product
                    {
                        ProductId = 2,
                        Name = "Beans"
                    },
                    Quantity = 2

                },
                new DiscountedProduct()
                {
                    ProductId = 4,
                    Name = "Milk"
                });

            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProducts()).ToArray();

            // Assert
            Assert.AreEqual(result.Any(), false);
        }

        [TestMethod]
        public void HalfPriceDiscount_NoDiscountApplied_WithQuantity_40rMore()
        {
            // Arrange
            var percentageDiscount = new HalfPriceDiscount(new ProductQuantity
                {
                    Product = new Product
                    {
                        ProductId = 2,
                        Name = "Beans"
                    },
                    Quantity = 4

                },
                new DiscountedProduct()
                {
                    ProductId = 4,
                    Name = "Milk"
                });

            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProducts()).ToArray();

            // Assert
            Assert.AreEqual(result.Any(), false);
        }        
    }
}
