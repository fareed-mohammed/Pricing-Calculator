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
    public class PercentageDiscountTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PercentageDiscount_DiscountedItems_IsNull_ExceptionThrown()
        {
            // Arrange + Act + Assert
            var percentageDiscount = new PercentageDiscount(null, 0.10m);
        }

        [TestMethod]
        public void PercentageDiscount_NoDiscountApplied_With10PercentDiscount()
        {
            // Arrange
            var percentageDiscount = new PercentageDiscount(new DiscountedProduct
            {
                ProductId = 4,
                Name = "Milk"
            }, 0.10m);

            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProducts()).ToArray();

            // Assert
            Assert.AreEqual(result.Any(), false);            
        }

        [TestMethod]
        public void PercentageDiscount_CalculateAppliedDiscount_With10PercentDiscount()
        {
            // Arrange
            var percentageDiscount = new PercentageDiscount(DiscountHelper.CreateDiscountedProducts(), 0.10m);            

            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProducts()).ToArray();

            // Assert
            Assert.AreEqual(result.Any(), true);
            Assert.AreEqual(result[0].Type, DiscountType.Percentage);
            Assert.AreEqual(result[0].Amount, 0.03m);
            Assert.AreEqual(result[0].Text, "Apples 10% OFF: - 3p");
        }

        [TestMethod]
        public void PercentageDiscount_CalculateAppliedDiscount_With50PercentDiscount()
        {
            // Arrange
            var percentageDiscount = new PercentageDiscount(DiscountHelper.CreateDiscountedProducts(), 0.50m);

            // Act
            var result = percentageDiscount.DiscountsApplicable(ProductQuantityHelper.CreateProducts()).ToArray();

            // Assert
            Assert.AreEqual(result.Any(), true);
            Assert.AreEqual(result[0].Type, DiscountType.Percentage);
            Assert.AreEqual(result[0].Amount, 0.16m);
            Assert.AreEqual(result[0].Text, "Apples 50% OFF: - 16p");
        }
    }
}
