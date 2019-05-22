using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodLog.Models.Recipe;

namespace FoodLog.RecipeData
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetRecipes();

    }
}
