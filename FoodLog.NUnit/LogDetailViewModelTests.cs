using System;
using FoodLog.FoodModels;
using FoodLog.ViewModels.LogFood;
using NUnit.Framework;

namespace FoodLog.NUnit
{
    public class LogDetailViewModelTests
    {
        [Test]
        public void ConstructorMustConstruct()
        {
            var f = new FoodPerDay();
            f.Food = new Food();
            f.Food.Name = "Name";

            var r = new LogDetailViewModel(f);
            Assert.IsNotNull(r);
            Assert.AreEqual("Name", r.Title);
            Assert.IsNull(r.FoodRepository);
            Assert.IsNull(r.RecipeRepository);
        }
    }
}
