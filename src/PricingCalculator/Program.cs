namespace PricingCalculator
{
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length == 0)
                {
                    Console.WriteLine("No products passed in!");
                    Console.ReadLine();
                }
                else
                {
                    var products = new List<string>();

                    foreach (var arg in args)
                    {
                        products.Add(arg.ToLower());
                    }

                    var discounts = Utilities.CreateDiscounts();

                    var basket = new ShoppingBasket(discounts);

                    basket.AddProducts(Utilities.CreateProducts(products));

                    var subTotal = basket.SubTotal;

                    var discountsApplied = basket.GetBasketDiscounts().ToArray();

                    var totalPrice = subTotal - discountsApplied.Sum(item => item.Amount);

                    Console.WriteLine("SubTotal : " + $"{subTotal.ToCurrencyString()}");

                    foreach (var discount in discountsApplied)
                    {
                        Console.WriteLine(discount.Text);
                    }

                    Console.WriteLine("Total UnitPrice : " + $"{totalPrice.ToCurrencyString()}");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                // Log error using some framework like NLog, Log4Net, etc

                Console.WriteLine(ex);
                throw new ApplicationException("An application error occurred", ex);
            }
        }        
    }
}
