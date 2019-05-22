using System;
using System.Diagnostics;
using FoodLog.ViewModels.LogFood;
using FoodLog.FoodModels;

namespace FoodLog.ViewModelLocator
{
    public static class ViewModelLocator
    {
        static LogDayViewModel dayViewModel;

        public static LogDayViewModel ItemsViewModel
        {
            get
            {
                var ret = dayViewModel ?? (dayViewModel = new LogDayViewModel());
                if (ret.BreakFast.Count == 0)
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
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
