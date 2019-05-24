using System;
using FoodLog.FoodModels;
using FoodLog.ViewModels.LogFood;
using NUnit.Framework;

namespace FoodLog.NUnit
{
    public class LogAddToDayModelTests
    {
        [Test]
        public void ConstructorMustConstruct()
        {
            var f = new FoodPerDay();
            var m = new LogAddToDayModel(f);

            Assert.IsNotNull(m);
            Assert.IsNotNull(m.FoodEntry);
            Assert.IsNull(m.FoodRepository);
            Assert.IsNull(m.RecipeRepository);
            Assert.AreEqual(100, m.FoodEntry.Amount);
        }
    }
}
