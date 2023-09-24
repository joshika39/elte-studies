using gyak8.Product;

namespace gyak8.Department
{
    public interface IDepartment
    {
        List<IProduct> Stock { get; }
    }
}
