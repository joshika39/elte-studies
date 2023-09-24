using gyak8.Department;
using gyak8.Product;

namespace gyak8
{
    public class Buyer
    {
        private readonly List<string> _shoppingList;
        private readonly List<IProduct> _shoppingCart;
        
        private Shop _shop;

        public Buyer()
        {
            _shoppingCart = new List<IProduct>();
        }
        
        public Buyer(List<string> shoppingList, Shop shop, params string[] items)
        {
            _shop = shop;
            _shoppingList = items.ToList();
            _shoppingCart = new List<IProduct>();

        }

        public Buyer(List<string> shoppingList, Shop shop)
        {
            _shoppingList = shoppingList;
            _shop = shop;
            _shoppingCart = new List<IProduct>();
        }
        
        public void Shopping(Shop shop, params string[] items)
        {
            _shop = shop ?? throw new ArgumentNullException(nameof(shop));
            
            Search()
        }

        public bool Search(string name, out IProduct result)
        {
            foreach (var grocery in _shop.Groceries.Stock.Where(grocery => grocery.GetName() == name))
            {
                result = grocery;
                return true;
            }
            
            foreach (var tech in _shop.Tech.Stock.Where(grocery => grocery.GetName() == name))
            {
                result = tech;
                return true;
            }
            
            result = default;
            return false;
        }
        
        public bool SearchCheap(string name, out IProduct result)
        {
            var tech = _shop.Tech.Stock.Where(grocery => grocery.GetName() == name).MinBy(p => p.GetPrice());
            if (tech is not null)
            {
                result = tech;
                return true;
            }
            result = default;
            return false;
        }

        private void Buy(IProduct product, IDepartment department)
        {
            switch (department)
            {
                case TechDepartment:
                    
                    break;
                case GroceryDepartment:
                    break;
            }
        }
    }
}
