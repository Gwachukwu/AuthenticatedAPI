using ShopAPI.Models;

namespace ShopAPITests.Models
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void CanInstantiateProductWithDefaultValues()
        {
            var product = new Product();
            Assert.IsNull(product.Name);
            Assert.IsNull(product.Price);
            Assert.IsNull(product.Description);
            Assert.IsNull(product.Category);
        }

        [TestMethod]
        public void CanSetProductProperties()
        {
            var category = new Category { Id = 2, Description = "Books" };
            var product = new Product
            {
                Id = 1,
                Name = "The Great Gatsby",
                Price = "15.99",
                Description = "Classic Novel",
                Category = category
            };

            Assert.AreEqual(1, product.Id);
            Assert.AreEqual("The Great Gatsby", product.Name);
            Assert.AreEqual("15.99", product.Price);
            Assert.AreEqual("Classic Novel", product.Description);
            Assert.AreEqual(category, product.Category);
        }

        [TestMethod]
        public void ProductInitializationWithCategorySetsCategoryCorrectly()
        {
            var category = new Category { Id = 1, Description = "Books" };
            var product = new Product
            {
                Id = 1,
                Name = "1984",
                Price = "9.99",
                Description = "Dystopian novel",
                Category = category
            };

            Assert.IsNotNull(product.Category);
            Assert.AreEqual(1, product.Category.Id);
            Assert.AreEqual("Books", product.Category.Description);
        }
        [TestMethod]
        public void UpdateProductCategory()
        {
            var category1 = new Category { Id = 1, Description = "Books" };
            var product = new Product { Id = 1, Name = "The Catcher in the Rye", Price = "20.00", Description = "Book", Category = category1 };

            var category2 = new Category { Id = 2, Description = "Electronics" };
            product.Category = category2; // Update category

            Assert.AreEqual(2, product.Category.Id);
            Assert.AreEqual("Electronics", product.Category.Description);
        }

    }
}
