using ShopAPI.Models;

namespace ShopAPITests.Models
{
    [TestClass]
    public class ShoppingCartTests
    {
        [TestMethod]
        public void CanInstantiateShoppingCartWithDefaultValues()
        {
            var cart = new ShoppingCart();
            Assert.IsNull(cart.User);
            Assert.AreEqual(0, cart.Id);
            Assert.AreEqual(0, cart.Products.Count);
        }

        [TestMethod]
        public void CanAddProductToShoppingCart()
        {
            var cart = new ShoppingCart();
            var product = new Product { Id = 1, Name = "Apple iPhone 12" };

            cart.Products.Add(product);

            Assert.AreEqual(1, cart.Products.Count);
            Assert.AreEqual(product, cart.Products.First());
        }
        [TestMethod]
        public void ShoppingCartRemoveProductWorksCorrectly()
        {
            var cart = new ShoppingCart();
            var product = new Product { Id = 1, Name = "Apple iPhone 12" };
            cart.Products.Add(product);

            // Now, remove the product
            cart.Products.Remove(product);
            Assert.AreEqual(0, cart.Products.Count);
        }

        [TestMethod]
        public void ShoppingCartCanBeInitializedWithUser()
        {
            var cart = new ShoppingCart { User = "user@example.com" };
            Assert.AreEqual("user@example.com", cart.User);
        }
        [TestMethod]
        public void ClearShoppingCartProducts()
        {
            var cart = new ShoppingCart();
            cart.Products.Add(new Product { Id = 1, Name = "Laptop" });
            cart.Products.Add(new Product { Id = 2, Name = "Smartphone" });

            cart.Products.Clear(); // Clear all products

            Assert.AreEqual(0, cart.Products.Count);
        }

    }
}
