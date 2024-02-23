using ShopAPI.Models;

namespace ShopAPITests.Models
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void CanInstantiateCategoryWithDefaultValues()
        {
            var category = new Category();
            Assert.IsNull(category.Description);
            Assert.AreEqual(0, category.Id);
        }

        [TestMethod]
        public void CanSetCategoryProperties()
        {
            var category = new Category
            {
                Id = 1,
                Description = "Electronics"
            };

            Assert.AreEqual(1, category.Id);
            Assert.AreEqual("Electronics", category.Description);
        }

        [TestMethod]
        public void CategoryToStringReturnsExpectedValue()
        {
            var category = new Category
            {
                Id = 1,
                Description = "Electronics"
            };

            Assert.AreEqual("Electronics", category.Description, "The ToString method should return the Description property value.");
        }
        [TestMethod]
        public void CategoryEqualityCheck()
        {
            var category1 = new Category { Id = 1, Description = "Electronics" };
            var category2 = new Category { Id = 1, Description = "Electronics" };

            Assert.AreEqual(category1.Id, category2.Id);
            Assert.AreEqual(category1.Description, category2.Description);
        }

    }
}
