using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodLog.FoodData;
using FoodLog.Models.Recipe;

namespace FoodLog.RecipeData
{
    public class RecipeRepository : IRecipeRepository
    {
        private IFoodDatabase foodDatabase;

        public RecipeRepository(IFoodDatabase foodDatabase)
        {
            this.foodDatabase = foodDatabase;
        }

        public async Task<List<Recipe>> GetRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();
            List<Models.DB.Recipe> fromDB = await this.foodDatabase.GetRecipes();

            foreach (var recipeFromDB in fromDB)
            {
                Recipe recipe = ConvertFromDb(recipeFromDB);
                recipes.Add(recipe);

                List<Models.DB.RecipeItem> items = await this.foodDatabase.GetRecipeItems(recipeFromDB.Id);

                foreach (var recipeItemDB in items)
                {
                    Models.DB.Food foodDb = await this.foodDatabase.FoodById(recipeItemDB.FoodId);

                    RecipeItem recipeItem = ConvertFromDb(recipeItemDB);
                    recipeItem.Food = FoodRepository.ConvertFromDb(foodDb);

                    recipe.Add(recipeItem);
                }
            }

            return recipes;
        }

        public static Models.Recipe.Recipe ConvertFromDb(Models.DB.Recipe recipeFromDB)
        {
            return new Models.Recipe.Recipe()
            {
                Id = recipeFromDB.Id,
                IsReadOnly = recipeFromDB.IsReadOnly,
                Name = recipeFromDB.Name
            };
        }

        public static Models.Recipe.RecipeItem ConvertFromDb(Models.DB.RecipeItem recipeItemDB)
        {
            return new Models.Recipe.RecipeItem()
            {
                Amount = recipeItemDB.Amount,
                Id = recipeItemDB.Id,
                RecipeId = recipeItemDB.RecipeId
            };
        }
    }
}
