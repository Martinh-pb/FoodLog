using System;
using FoodLog.FoodData;
using FoodLog.ViewModels.LogFood;
using Moq;
using NUnit.Framework;

namespace FoodLog.NUnit
{
    public class LogDayViewModelTests
    {
        [Test]
        public void ConstructorMustConstruct()
        {
            var r = new Mock<IFoodRepository>();
            var m = new LogDayViewModel(r.Object);
            Assert.IsNotNull(m);
            Assert.IsNotNull(m.FoodRepository);
            Assert.IsNull(m.RecipeRepository);
            Assert.IsNotNull(m.Items);
            Assert.AreEqual(4, m.Items.Count);
            Assert.IsNotNull(m.BreakFast); 
            Assert.IsNotNull(m.Lunch); 
            Assert.IsNotNull(m.Diner); 
            Assert.IsNotNull(m.Snack);
        }
    }
}
