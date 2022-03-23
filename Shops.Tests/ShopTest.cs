using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shops.Entities;


namespace Shops.Tests
{
    public class ShopTest
    {
        private ShopManager _shopManager;
        
        [SetUp]
        public void Setup()
        {
            _shopManager = new Entities.ShopManager();
        }
        
        [Test]
        public void DeliveryOfGoodsToTheStore()
        {
            const double moneyBefore = 1000;
            const double productPrice = 100;
            const uint productCount = 11;
            const uint productToBuyCount = 9;
            const double moneyAfter = moneyBefore - productPrice * productToBuyCount;
            const uint productCountAfter = productCount - productToBuyCount;
            
            var person = new Person("name", moneyBefore);
            Shop shop = _shopManager.Create("shop name", "Chkalovskiy prospekt 50");
            Product protoTypeProduct = _shopManager.RegisterProduct("product name");
            
            Product product = shop.AddProduct(protoTypeProduct, productPrice, productCount);
            shop.Buy(person, product, productToBuyCount);
	
            Assert.AreEqual(moneyAfter, person.GetMoney());
            Assert.AreEqual(productCountAfter , shop.GetProductInfo(product).GetCount());
        }
        
        [Test]
        public void SettingAndChangingPricesForSomeProductInTheStore()
        {
            const double oldPrice = 100;
            const double newPrice = 120;
            Shop shop = _shopManager.Create("shop name", "Chkalovskiy prospekt 50");
            Product protoTypeProduct = _shopManager.RegisterProduct("product name");
            
            Product product = shop.AddProduct(protoTypeProduct, oldPrice, 10);
            product.NewPrice(newPrice);
	
            Assert.AreEqual(newPrice, product.GetPrice());
        }
        
        [Test]
        public void SearchForStoreWhereBatchOfGoodsCanBeBoughtAsCheaplyAsPossible()
        {
            var person = new Person("name", 1000);
            Shop shop1 = _shopManager.Create("Lenta", "Chkalovskiy prospekt 50");
            Shop shop2 = _shopManager.Create("Dicsi", "Chkalovskiy prospekt 51");
            Shop shop3 = _shopManager.Create("Azbuka Vkusov", "Chkalovskiy prospekt 52");
            Product protoTypeProduct = _shopManager.RegisterProduct("product name");
            
            shop1.AddProduct(protoTypeProduct, 100, 7);
            shop2.AddProduct(protoTypeProduct, 90, 8);
            shop3.AddProduct(protoTypeProduct, 80, 4);
            Shop shopWithMinPrices = _shopManager.BuyProduct(protoTypeProduct, 5);
	
            Assert.AreEqual(shopWithMinPrices.GetName(), "Dicsi");
        }
        
        [Test]
        public void PurchaseOfBatchOfGoodsInStore()
        {
            var person = new Person("Bair", 10000);
            Shop shop = _shopManager.Create("Lenta", "Chkalovskiy prospekt 50");
            Product protoTypeProduct1 = _shopManager.RegisterProduct("banana");
            Product protoTypeProduct2 = _shopManager.RegisterProduct("meat");
            Product protoTypeProduct3 = _shopManager.RegisterProduct("water");

            var productAndCount = new ProductsAndCount();
            Product product1 = shop.AddProduct(protoTypeProduct1, 60, 10);
            Product product2 = shop.AddProduct(protoTypeProduct2, 200, 3);
            Product product3 = shop.AddProduct(protoTypeProduct3, 40, 5);
            
            productAndCount.Add(product1, 9);
            productAndCount.Add(product2, 1);
            productAndCount.Add(product3, 5);
            shop.Buy(person, productAndCount);
            
            Assert.AreEqual(1, shop.GetProductInfo(product1).GetCount());
            Assert.AreEqual(2, shop.GetProductInfo(product2).GetCount());
            Assert.AreEqual(0, shop.GetProductInfo(product3).GetCount());
        }
    }
}