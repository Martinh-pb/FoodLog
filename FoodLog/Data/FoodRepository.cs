using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodLog.FoodModels;

namespace FoodLog.Data
{
    public class FoodRepository : IFoodRepository
    {
        private FoodDatabase foodDatabase;

        public FoodRepository(FoodDatabase foodDatabase)
        {
            this.foodDatabase = foodDatabase;
        }

        public async Task<List<FoodPerDay>> GetFoodForDay(DateTime date)
        {
            List<FoodPerDay> foodPerDays = new List<FoodPerDay>();

            List<Models.DB.FoodDayEntry> r = await foodDatabase.GetForDay(date);
            Dictionary<int, Food> cachedFoods = new Dictionary<int, Food>();

            foreach (Models.DB.FoodDayEntry e in r)
            {
                Food food;
                if (!cachedFoods.TryGetValue(e.FoodId, out food))
                {
                    food = await FoodById(e.FoodId);
                    cachedFoods[food.Id] = food;
                }

                FoodPerDay pd = ConvertFromDb(e, food);
                foodPerDays.Add(pd);
            }

            return foodPerDays;
        }

        public async Task<Food> FoodById(int id)
        {
            Models.DB.Food dbFood = await foodDatabase.FoodById(id);
            return ConvertFromDb(dbFood);
        }

        public async Task<int> SaveFoodForDay(FoodPerDay foodPerDay)
        {
            var dbm = ConvertToDb(foodPerDay);
            return await foodDatabase.SaveFoodForDay(dbm);
        }

        public async Task<int> DeleteFoodForDay(FoodPerDay foodPerDay)
        {
            var entry = ConvertToDb(foodPerDay);
            return await foodDatabase.DeleteFoodForDay(entry);
        }

        public async Task<List<Food>> GetFoods()
        {
            List<Food> all = new List<Food>();
            var g = await foodDatabase.GetAllTheFood();

            g.ForEach(t => all.Add(ConvertFromDb(t)));
            return all;
        }

        public async Task<List<Food>> FindFood(string search)
        {
            List<Food> all = new List<Food>();

            var g = await foodDatabase.FindFood(search);

            g.ForEach(t => all.Add(ConvertFromDb(t)));
            return all;
        }

        public async Task<int> SaveFood(Food food)
        {
            Models.DB.Food dbFood = ConvertToDb(food);
            return await foodDatabase.SaveFood(dbFood);
        }

        public static Food ConvertFromDb(Models.DB.Food dbFood)
        {
            return new Food()
            {
                Id = dbFood.Id,
                Calories = dbFood.Calories,
                Carbs = dbFood.Carbs,
                Fat = dbFood.Fat,
                Fiber = dbFood.Fiber,
                Name = dbFood.Name,
                Natrium = dbFood.Natrium,
                Portion = dbFood.Portion,
                PortionType = (PortionType)dbFood.PortionType,
                Protein = dbFood.Protein,
                SaturatedFat = dbFood.SaturatedFat,
                Sugar = dbFood.Sugar,
                BarCode = dbFood.BarCode
            };
        }
        public static FoodPerDay ConvertFromDb(Models.DB.FoodDayEntry dayEntry, Food food)
        {
            return new FoodPerDay()
            {
                Id = dayEntry.Id,
                Date = dayEntry.Date,
                Amount = dayEntry.Amount,
                Food = food,
                MealType = (MealType)dayEntry.MealType,
            };
        }

        public static Models.DB.FoodDayEntry ConvertToDb(FoodPerDay foodPerDay)
        {
            return new Models.DB.FoodDayEntry()
            {
                Id = foodPerDay.Id,
                Amount = foodPerDay.Amount,
                Date = foodPerDay.Date,
                FoodId = foodPerDay.Food.Id,
                MealType = (int)foodPerDay.MealType,
                Time = foodPerDay.Time
            };
        }

        public static Models.DB.Food ConvertToDb(Food food)
        {
            return new Models.DB.Food()
            {
                Id = food.Id,
                Calories = food.Calories,
                Carbs = food.Carbs,
                Fat = food.Fat,
                Fiber = food.Fiber,
                Name = food.Name,
                Natrium = food.Natrium,
                Portion = food.Portion,
                PortionType = (int)food.PortionType,
                Protein = food.Protein,
                SaturatedFat = food.SaturatedFat,
                Sugar = food.Sugar,
                BarCode = food.BarCode
            };
        }
    }
}
