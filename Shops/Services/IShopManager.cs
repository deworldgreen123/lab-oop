using Shops.Entities;

namespace Shops.Services
{
    public interface IShopManager
    {
        Shop Create(string shopName, string shopAddress);

        Product RegisterProduct(string productName);

        Shop BuyProduct(Product product, uint count = 0);
    }
}