using System;
using System.Diagnostics;
using FoodLog.ViewModels.LogFood;
using FoodLog.FoodModels;
using FoodLog.FoodData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodLog.ViewModelLocator
{
    public static class ViewModelLocator
    {
        static LogDayViewModel dayViewModel;

        public static LogDayViewModel ItemsViewModel
        {
            get
            {
                var ret = dayViewModel ?? (dayViewModel = new LogDayViewModel(new DummyFoodRepository()));
                CreateItemsViewModelData(ret);
                ret.BreakFast.Calculated = ret.BreakFast.Calcutate();
                return ret;

            }
        }

        public static void CreateItemsViewModelData(LogDayViewModel item)
        {
            var pd = new FoodPerDay()
            {
                Amount = 12,
                Date = DateTime.Today,
                Id = 1,
                MealType = MealType.BreakFast,
                Time = DateTime.Today
            };
            var food = new Food() { Id = 2, Name = "Paprika", Portion = 50, Calories = 50, Fat = 10, Protein = 5, Carbs = 2 };
            pd.Food = food;
            try
            {
                item.BreakFast.Add(pd);
                item.BreakFast.Add(new FoodPerDay() { Id = -1, MealType = MealType.BreakFast });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }

    public class DummyFoodRepository : IFoodRepository
    {
        public Task<int> DeleteFoodForDay(FoodPerDay foodPerDay)
        {
            throw new NotImplementedException();
        }

        public Task<List<Food>> FindFood(string search)
        {
            throw new NotImplementedException();
        }

        public Task<Food> FoodById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FoodPerDay>> GetFoodForDay(DateTime date)
        {
            List<FoodPerDay> foodPerDays = new List<FoodPerDay>();

            var pd = new FoodPerDay()
            {
                Amount = 12,
                Date = DateTime.Today,
                Id = 1,
                MealType = MealType.BreakFast,
                Time = DateTime.Today
            };
            var food = new Food() { Id = 2, Name = "Paprika", Portion = 50, Calories = 50, Fat = 10, Protein = 5, Carbs = 2 };
            pd.Food = food;

            foodPerDays.Add(pd);

            return System.Threading.Tasks.Task.FromResult(foodPerDays);
        }

        public Task<List<Food>> GetFoods()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveFood(Food food)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveFoodForDay(FoodPerDay foodPerDay)
        {
            throw new NotImplementedException();
        }
    }
}
