using System.Security;

namespace gyak8.Product
{
    public class Grocery : IProduct
    {
        public Guid Id { get; }
        private readonly string _name;
        private int _price;
        
        public Grocery(string name, int price)
        {
            Id = Guid.NewGuid();
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _price = price;
        }
        
        public string GetName()
        {
            return _name;
        }
        
        public int GetPrice()
        {
            return _price;
        }
        
        public void SetPrice(SecureString adminPass, int newPrice)
        {
            if (adminPass == new SecureString())
            {
                // do some checks
                _price = newPrice;
            }
        }
    }
}
