namespace PricingCalculator.Builders
{
    using PricingCalculator.Domain;

    public class ProductBuilder
    {       
        private int _productId;
        private string _name;
        private decimal _unitPrice;

        public ProductBuilder Create(string name)
        {
            _name = name;
            return this;
        }
        
        public ProductBuilder WithProductId(int productId)
        {
            _productId = productId;
            return this;
        }

        public ProductBuilder WithUnitPrice(decimal unitPrice)
        {
            _unitPrice = unitPrice;
            return this;
        }

        public static implicit operator Product(ProductBuilder pb)
        {
            return new Product
            {
                ProductId = pb._productId,
                Name = pb._name,
                UnitPrice = pb._unitPrice
            };               
        }
    }
}
