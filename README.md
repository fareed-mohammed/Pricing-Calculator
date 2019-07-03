# Pricing Calculator

Programming Challenge: Price Calculator for Shopping Basket
Expectation
- Produce working, object-oriented source code to solve the following problems
- Should be send back in electronic format as a complete project with related unit tests
- We will walk through your code together in the next session, answering questions on the code
and programming/design choices you made.

Description
Write a program and associated unit tests that can price a basket of goods taking into account
some special offers.

The goods that can be purchased, together with their normal prices are:
- Beans – 65p per can
- Bread – 80p per loaf
- Milk – £1.30 per bottle
- Apples – £1.00 per bag

Current special offers:
- Apples have a 10% discount off their normal price this week
- Buy 2 cans of Bean and get a loaf of bread for half price

The program should accept a list of items in the basket and output the subtotal, the special
offer discounts and the final price.

Input should be via the command line in the form
PriceCalculator item1 item2 item3 …

For example:
&gt; PriceCalculator Apple Milk Bread

Output should be to the console, for example:
Subtotal: £3.10
Apples 10% off: -10p
Total: £3.00

If no special offers are applicable the code should output:
Subtotal: £1.30
(No offers available)
Total price: £1.30

The code and design should meet these requirements, but be sufficiently flexible to allow future
changes to the product list and/or discounts applied.

The code should be well structured, commented, have error handling and be tested
