using System;

namespace Shops.Entities
{
    public class Product
    {
        private string _name;
        private double _price = 0;
        private uint _id;
        private uint _count = 0;

        public Product(string name, uint id)
        {
            _name = name;
            _id = id;
        }

        public Product(Product product, double price, uint count)
        {
            _name = product._name;
            _price = price;
            _id = product._id;
            _count = count;
        }

        public double GetPrice()
        {
            return _price;
        }

        public double GetId()
        {
            return _id;
        }

        public uint GetCount()
        {
            return _count;
        }

        public double NewPrice(double newPrice)
        {
            _price = newPrice;
            return _price;
        }

        public uint NewCount(uint newCount)
        {
            _count = newCount;
            return _count;
        }
    }
}