using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private uint _id;
        private string _name;
        private string _address;
        private List<Product> _products;

        public Shop(uint id, string name, string address)
        {
            _id = id;
            _name = name;
            _address = address;
            _products = new List<Product>();
        }

        public Product AddProduct(Product product, double price, uint count)
        {
            Product findProduct = _products.Find(item => item.GetId() == product.GetId());
            if (findProduct == null)
            {
                product = new Product(product, price, count);
                _products.Add(product);
                return product;
            }

            findProduct.NewCount(count + findProduct.GetCount());
            findProduct.NewPrice(price);
            return findProduct;
        }

        public void Buy(Person person, Product product, uint count)
        {
            Product findProduct = _products.Find(item => item.GetId() == product.GetId());
            if (findProduct == null)
            {
                throw new ShopsException("YOUR_ERROR: the product is not in the store");
            }

            if (findProduct.GetCount() < count)
            {
                throw new ShopsException("YOUR_ERROR: there are not enough products");
            }

            if (!(person.GetMoney() >= findProduct.GetPrice() * count) || findProduct.GetCount() < count)
            {
                throw new ShopsException("YOUR_ERROR: You don't have enough money");
            }

            person.Withdrawal(findProduct.GetPrice() * count);
            findProduct.NewCount(findProduct.GetCount() - count);
        }

        public void Buy(Person person, ProductsAndCount products)
        {
            double productReceipt = 0;
            foreach ((Product product, uint count) in products)
            {
                Product findProduct = _products.Find(item => item.GetId() == product.GetId());
                if (findProduct == null)
                {
                    throw new ShopsException("YOUR_ERROR: the product is not in the store");
                }

                if (count > findProduct.GetCount())
                {
                    throw new ShopsException("YOUR_ERROR: the product is not in the store");
                }

                productReceipt += findProduct.GetPrice() * count;
            }

            if (productReceipt > person.GetMoney())
            {
                throw new ShopsException("YOUR_ERROR: You don't have enough money");
            }

            person.Withdrawal(productReceipt);
            foreach ((Product product, uint count) in products)
            {
                Product findProduct = _products.Find(item => item.GetId() == product.GetId());
                findProduct.NewCount(findProduct.GetCount() - count);
            }
        }

        public Product GetProductInfo(Product product)
        {
            Product findProduct = _products.Find(item => item.GetId() == product.GetId());
            return findProduct;
        }

        public uint GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetAddress()
        {
            return _address;
        }
    }
}