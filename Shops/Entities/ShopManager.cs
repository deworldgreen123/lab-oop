using System.Collections.Generic;
using Shops.Services;
using Shops.Tools;

namespace Shops.Entities
{
    public class ShopManager : IShopManager
    {
    private List<Shop> _shops;
    private uint _nextIdShop = 0;
    private uint _nextIdProduct = 0;

    public ShopManager()
    {
        _shops = new List<Shop>();
    }

    public Shop Create(string shopName, string shopAddress)
    {
        var newShop = new Shop(_nextIdShop++, shopName, shopAddress);
        _shops.Add(newShop);
        return newShop;
    }

    public Product RegisterProduct(string productName)
    {
        var newProduct = new Product(productName, _nextIdProduct++);
        return newProduct;
    }

    public Shop BuyProduct(Product product, uint count = 0)
    {
        double minPrice = double.MaxValue;
        int index = 0;
        for (int i = 0; i < _shops.Count; i++)
        {
            double minShop = _shops[i].GetProductInfo(product).GetPrice();
            uint countMinShop = _shops[i].GetProductInfo(product).GetCount();
            if (minPrice > minShop && count <= countMinShop)
            {
                minPrice = minShop;
                index = i;
            }
        }

        if (minPrice == double.MaxValue)
        {
            throw new ShopsException("YOUR_ERROR: the product is missing in all stores or there is not enough of it");
        }
        else
        {
            return _shops[index];
        }
    }
    }
}