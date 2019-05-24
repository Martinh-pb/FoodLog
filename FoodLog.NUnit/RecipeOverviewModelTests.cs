using System;
using FoodLog.FoodData;
using FoodLog.RecipeData;
using FoodLog.ViewModels.Recipe;
using Moq;
using NUnit.Framework;

namespace FoodLog.NUnit
{
    public class RecipeOverviewModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructorMustConstruct()
        {
            var repo = new Mock<IRecipeRepository>();
            var r = new RecipeOverviewViewModel(repo.Object);

            Assert.IsNotNull(r);
            Assert.IsNotNull(r.RecipeRepository);
        }
    }
}
