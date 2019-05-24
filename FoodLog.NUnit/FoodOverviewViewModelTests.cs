using System;
using FoodLog.FoodData;
using FoodLog.ViewModels;
using Moq;
using NUnit.Framework;

namespace FoodLog.NUnit
{
    public class FoodOverviewViewModelTests
    {
        [Test]
        public void ConstructorMustConstruct()
        {
            var repo = new Mock<IFoodRepository>();
            var model = new FoodOverviewViewModel(repo.Object);
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.FoodRepository);
            Assert.IsNull(model.RecipeRepository);
        }
    }
}
