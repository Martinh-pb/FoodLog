using FoodLog.FoodData;
using FoodLog.FoodModels;
using NUnit.Framework;

namespace FoodLog.Tests
{
    public class FoodRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConvertFromDbMustReturnAValidFoodObject()
        {
            var f = new FoodLog.Models.DB.Food()
            {
                BarCode = "12",
                Calories = 10,
                Carbs = 11,
                Fat = 12,
                Fiber = 13,
                Id = 14,
                Name = "Name",
                Natrium = 15,
                Portion = 1,
                PortionType = 1,
                Protein = 16,
                SaturatedFat = 17,
                Sugar = 18
            };

            var res = FoodRepository.ConvertFromDb(f);
            Assert.IsNotNull(res);
            Assert.IsInstanceOf(typeof(Food), res);
            Assert.AreEqual("12", res.BarCode);
            Assert.AreEqual(10, res.Calories);
            Assert.AreEqual(11, res.Carbs);
            Assert.AreEqual(12, res.Fat);
            Assert.AreEqual(13, res.Fiber);
            Assert.AreEqual(14, res.Id);
            Assert.AreEqual("Name", res.Name);
            Assert.AreEqual(15, res.Natrium);
            Assert.AreEqual(1, res.Portion);
            Assert.AreEqual(PortionType.Ml, res.PortionType);
            Assert.AreEqual(16, res.Protein);
            Assert.AreEqual(17, res.SaturatedFat);
            Assert.AreEqual(18, res.Sugar);
        }
    }
}