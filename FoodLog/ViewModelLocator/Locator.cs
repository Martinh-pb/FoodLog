using System;
using System.Diagnostics;
using FoodLog.ViewModels.LogFood;
using FoodLog.FoodModels;
using FoodLog.FoodData;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodLog.ViewModels;
using FoodLog.ViewModels.Recipe;

namespace FoodLog.ViewModelLocator
{
    public static class ViewModelLocator
    {
        static LogDayViewModel dayViewModel;
        static FoodOverviewViewModel foodOverviewModel;
        static RecipeOverviewViewModel recipeOverviewModel;

        public static RecipeOverviewViewModel RecipeOverviewModel
        { 
            get
            {
                var ret = recipeOverviewModel ?? (recipeOverviewModel = new RecipeOverviewViewModel(null));
                if (ret.Recipes.Count == 0)
                {
                    ret.Recipes.Add(new Models.Recipe.Recipe() {Id = 1, Name = "Keto baked omelet with bacon"});
                    ret.Recipes.Add(new Models.Recipe.Recipe() { Id = 2, Name = "Keto mayonaise" });
                }

                return ret;
            }
        }

        public static FoodOverviewViewModel FoodOverviewModel
        {
            get
            {
                var ret = foodOverviewModel ?? (foodOverviewModel = new FoodOverviewViewModel(null));
                if (ret.Foods.Count == 0)
                {
                    ret.Foods.Add(new Food() { Id = 1, Calories = 10, Name = "Spinazie", Portion = 100, PortionType = PortionType.Gram});
                    ret.Foods.Add(new Food() { Id = 2, Calories = 20, Name = "Ribeye (Deen)", Portion = 100, PortionType = PortionType.Gram });
                }
                return ret;
            } 
        }

        public static LogDayViewModel ItemsViewModel
        {
            get
            {
                var ret = dayViewModel ?? (dayViewModel = new LogDayViewModel(null));
                if (ret.Items.Count == 0)
                {
                    ret.Date = DateTime.Today;

                    var brk = new FoodDayGroup("Ontbijt", MealType.BreakFast);
                    ret.Items.Add(brk);
                    ret.BreakFast = brk;
                    CreateItemsViewModelData(brk);
                    //Lunch = new FoodDayGroup("Lunch", MealType.Lunch);
                    //Items.Add(Lunch);

                    //Diner = new FoodDayGroup("Diner", MealType.Diner);
                    //Items.Add(Diner);

                    //Snack = new FoodDayGroup("Snack", MealType.Snack);
                    //Items.Add(Snack);

                    //CreateItemsViewModelData(ret);
                    ret.BreakFast.Calcutate();
                }
                return ret;

            }
        }

        public static void CreateItemsViewModelData(FoodDayGroup grp)
        {
            var pd = new FoodPerDay()
            {
                Amount = 12,
                Date = DateTime.Today,
                Id = 1,
                MealType = MealType.BreakFast,
                Time = DateTime.Today,
                Food = new Food() { Id = 2, Name = "Paprika", Portion = 100, Calories = 50, Fat = 10, Protein = 5, Carbs = 2 }
            };

            var pd2 = new FoodPerDay()
            {
                Amount = 40,
                Date = DateTime.Today,
                Id = 2,
                MealType = MealType.BreakFast,
                Time = DateTime.Today,
                Food = new Food() { Id = 3, Name = "Spinazie", Portion = 100, Calories = 30, Fat = 0, Protein = 1, Carbs = 2 }
            };

            try
            {
                grp.Add(pd);
                grp.Add(pd2);
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
