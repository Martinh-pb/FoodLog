using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodLog.FoodModels;

namespace FoodLog.FoodData
{
    public interface IFoodRepository
    {
        Task<List<FoodPerDay>> GetFoodForDay(DateTime date);
        Task<Food> FoodById(int id);
        Task<int> SaveFoodForDay(FoodPerDay foodPerDay);
        Task<int> DeleteFoodForDay(FoodPerDay foodPerDay);
        Task<List<Food>> GetFoods();
        Task<List<Food>> FindFood(string search);
        Task<int> SaveFood(Food food);
    }
}
