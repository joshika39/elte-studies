using gyak8.Department;

namespace gyak8
{
    public class Shop
    {
        public IDepartment Groceries { get; }
        public IDepartment Tech { get; }

        public Shop()
        {
            Groceries = new GroceryDepartment();
            Tech = new TechDepartment();
        }
    }
}
