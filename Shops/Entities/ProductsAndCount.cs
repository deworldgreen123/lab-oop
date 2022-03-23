using System;
using System.Collections;
using System.Collections.Generic;

namespace Shops.Entities
{
    public class ProductsAndCount : Dictionary<Product, uint>
    {
        public Product Product { get; set; }
        public new uint Count { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProductsAndCount);
        }

        public bool Equals(ProductsAndCount other)
        {
            return other != null &&
                   Product == other.Product &&
                   Count == other.Count;
        }

        public override int GetHashCode()
        {
            return Convert.ToInt32(Product.GetId());
        }

        public void Deconstruct(out Product product, out uint count)
        {
            product = this.Product;
            count = this.Count;
        }
    }
}