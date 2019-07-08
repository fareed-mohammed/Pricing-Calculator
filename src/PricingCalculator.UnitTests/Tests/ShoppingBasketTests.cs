namespace PricingCalculator.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using PricingCalculator.Discounts;
    using PricingCalculator.Domain;
    using PricingCalculator.Enums;
    using PricingCalculator.UnitTests.Helpers;

    [TestClass]
    public class ShoppingBasketTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShoppingBasket_Discounts_IsNull_ExceptionThrown()
        {
            // Arrange + Act + Assert
            var shoppingBasket = new ShoppingBasket(null);
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(8)]
        [DataRow(5)]
        public void ShoppingBasket_AddProducts_CheckProductsCount(int productsToCreate)
        {
            // Arrange             
            var shoppingBasket = new ShoppingBasket(new List<IDiscount>());
            shoppingBasket.AddProducts(ProductQuantityHelper.CreateProducts(productsToCreate));

            // Act
            var result = shoppingBasket.ProductCount;

            // Assert
            Assert.AreEqual(result, productsToCreate);   
        }

        [TestMethod]        
        public void ShoppingBasket_CheckCalculateSubTotal_WithNoDiscounts()
        {
            // Arrange             
            var shoppingBasket = new ShoppingBasket(new List<IDiscount>());
            shoppingBasket.AddProducts(ProductQuantityHelper.CreateProducts());

            // Act
            var result = shoppingBasket.SubTotal;

            // Assert
            Assert.AreEqual(result, 3.23m);
        }

        [TestMethod]
        public void ShoppingBasket_CheckCalculateTotalPrice_WithNoDiscounts()
        {
            // Arrange             
            var shoppingBasket = new ShoppingBasket(new List<IDiscount>());
            shoppingBasket.AddProducts(ProductQuantityHelper.CreateProducts());

            // Act
            var result = shoppingBasket.SubTotal - shoppingBasket.GetBasketDiscounts().Sum(item => item.Amount);

            // Assert
            Assert.AreEqual(result, 3.23m);
        }

        [TestMethod]
        public void ShoppingBasket_CheckCalculateTotalPrice_WithPercentageDiscount()
        {
            // Arrange
            var percentageDiscount = new Mock<IDiscount>();

            percentageDiscount.Setup(mock => mock.DiscountsApplicable(It.IsAny<IEnumerable<ProductQuantity>>()))
                .Returns(DiscountHelper.CreatePercentageAppliedDiscount());

            var shoppingBasket = new ShoppingBasket(new List<IDiscount>{ percentageDiscount.Object});            
            shoppingBasket.AddProducts(ProductQuantityHelper.CreateProducts());

            // Act
            var discountsTotal = shoppingBasket.GetBasketDiscounts().Sum(item => item.Amount);
            var result = shoppingBasket.SubTotal - discountsTotal;

            // Assert
            Assert.AreEqual(result, 3.20m);
        }

        [TestMethod]
        public void ShoppingBasket_CheckCalculateTotalPrice_WithHalfPriceDiscount()
        {
            // Arrange         
            var halfPriceDiscount = new Mock<IDiscount>();

            halfPriceDiscount.Setup(mock => mock.DiscountsApplicable(It.IsAny<IEnumerable<ProductQuantity>>()))
                .Returns(DiscountHelper.CreateHalfPriceAppliedDiscount);

            var shoppingBasket = new ShoppingBasket(new List<IDiscount> { halfPriceDiscount.Object });
            shoppingBasket.AddProducts(ProductQuantityHelper.CreateProducts());

            // Act
            var discountsTotal = shoppingBasket.GetBasketDiscounts().Sum(item => item.Amount);
            var result = shoppingBasket.SubTotal - discountsTotal;

            // Assert
            Assert.AreEqual(result, 2.83m);
        }

        [TestMethod]
        public void ShoppingBasket_CheckCalculateTotalPrice_WitMultipleDiscounts()
        {
            // Arrange    
            var multlpleDiscounts = new Mock<IDiscount>();

            multlpleDiscounts.Setup(mock => mock.DiscountsApplicable(It.IsAny<IEnumerable<ProductQuantity>>()))
                .Returns(DiscountHelper.CreateMultipleAppliedDiscounts());

            var shoppingBasket = new ShoppingBasket(new List<IDiscount> { multlpleDiscounts.Object });
            shoppingBasket.AddProducts(ProductQuantityHelper.CreateProducts());

            // Act
            var discountsTotal = shoppingBasket.GetBasketDiscounts().Sum(item => item.Amount);
            var result = shoppingBasket.SubTotal - discountsTotal;

            // Assert
            Assert.AreEqual(result, 2.80m);
        }

        [TestMethod]
        public void ShoppingBasket_GetBasketDiscounts_WithNoDiscountApplied()
        {
            // Arrange
            var discounts = new Mock<IDiscount>();

            discounts.Setup(mock => mock.DiscountsApplicable(It.IsAny<IEnumerable<ProductQuantity>>()))
                .Returns(DiscountHelper.CreateNoDiscountApplied);

            var shoppingBasket = new ShoppingBasket(new List<IDiscount> { discounts.Object });
            shoppingBasket.AddProducts(ProductQuantityHelper.CreateProducts());

            // Act
            var result = shoppingBasket.GetBasketDiscounts().ToArray();
            
            // Assert
            Assert.AreEqual(result.Any(), true);
            Assert.AreEqual(result[0].Type, DiscountType.None);
            Assert.AreEqual(result[0].Amount, 0.00m);
            Assert.AreEqual(result[0].Text, "(No offers available)");
        }
    }
}
