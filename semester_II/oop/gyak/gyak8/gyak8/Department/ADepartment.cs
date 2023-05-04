using gyak8.Product;

namespace gyak8.Department
{
    public abstract class ADepartment : IDepartment
    {

        public List<IProduct> Stock { get; }

        protected ADepartment()
        {
            Stock = new List<IProduct>();
        }
    }
}
