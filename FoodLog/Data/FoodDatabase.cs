using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using FoodLog.Models.DB;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace FoodLog.Data
{
    public class FoodDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public FoodDatabase(string path)
        {
            _database = new SQLiteAsyncConnection(path);

            _database.CreateTableAsync<Target>().Wait();
            _database.CreateTableAsync<Food>().Wait();
            _database.CreateTableAsync<RecipeItem>().Wait();
            _database.CreateTableAsync<Recipe>().Wait();
            _database.CreateTableAsync<FoodDayEntry>().Wait();
        }

        public Task<List<FoodDayEntry>> GetAll()
        {
            var r = _database.Table<FoodDayEntry>().ToListAsync();
            return r;
        }

        public Task<List<FoodDayEntry>> GetForDay(DateTime date)
        {


            var r = _database.QueryAsync<FoodDayEntry>("SELECT * FROM [FoodDayEntry] WHERE [Date] = ?", date);
            //var r = _database.GetAllWithChildrenAsync<FoodDayEntry>(d => d.Date == date);

            return r;
        }

        public Task<int> SaveFoodForDay(FoodDayEntry entry)
        {
            try
            {
                if (entry.Id == 0)
                {
                    return _database.InsertAsync(entry);
                }
                else
                {
                    return _database.UpdateAsync(entry);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
                throw;
            }
        }

        public Task<int> DeleteFoodForDay(FoodDayEntry entry)
        {
            return _database.DeleteAsync(entry);
        }

        public Task<List<Food>> FindFood(string search)
        {
            return _database.Table<Food>().Where(f => f.Name.Contains(search)).ToListAsync();
        }

        public Task<Food> FoodById(int Id)
        {
            var food = _database.Table<Food>().FirstOrDefaultAsync(f => f.Id == Id);
            return food;
        }

        public Task<List<Food>> GetAllTheFood()
        {
            return _database.Table<Food>().ToListAsync();

        }

        public Task<int> SaveFood(Food entry)
        {
            try
            {
                if (entry.Id == 0)
                {
                    return _database.InsertAsync(entry);
                }
                else
                {
                    return _database.UpdateAsync(entry);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
                throw;
            }
        }
    }
}
