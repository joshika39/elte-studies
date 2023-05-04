using System.Security;

namespace gyak8.Product
{
    public interface IProduct
    {
        string GetName();
        int GetPrice();
        void SetPrice(SecureString adminPass, int newPrice);
    }
}
