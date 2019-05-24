using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodLog.Models.DB;

namespace FoodLog.FoodData
{
    public interface IFoodDatabase
    {
        Task<int> DeleteFoodForDay(FoodDayEntry entry);
        Task<List<Food>> FindFood(string search);
        Task<Food> FoodById(int Id);
        Task<List<FoodDayEntry>> GetAll();
        Task<List<Food>> GetAllTheFood();
        Task<List<FoodDayEntry>> GetForDay(DateTime date);
        Task<List<RecipeItem>> GetRecipeItems(int fromRecipeId);
        Task<List<Recipe>> GetRecipes();
        Task<int> SaveFood(Food entry);
        Task<int> SaveFoodForDay(FoodDayEntry entry);
    }
}