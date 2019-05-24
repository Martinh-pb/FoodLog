using System;
using FoodLog.FoodData;
using FoodLog.FoodModels;
using FoodLog.ViewModels.LogFood;
using Moq;
using NUnit.Framework;

namespace FoodLog.NUnit
{
    public class LogAddViewModelTests
    {
        [Test]
        public void ConstructorMustConstruct()
        {
            var f = new FoodPerDay();
            var r = new Mock<IFoodRepository>();
            var m = new LogAddViewModel(r.Object, f);
            Assert.IsNotNull(m);
            Assert.IsNotNull(m.Item);
            Assert.IsNotNull(m.SelectableFoods);
            Assert.IsNotNull(m.FoodRepository);
            Assert.IsNull(m.RecipeRepository);
            Assert.AreEqual(MealType.BreakFast, m.SelectedMealType.MealType);
        }
    }
}
