using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FoodLog.Models.DB
{
    public class Target
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Calories { get; set; }
        public double CarbsPercentage { get; set; }
        public double ProteinPercentage { get; set; }
        public double FatPercentage { get; set; }
    }

    public class FoodDayEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int MealType { get; set; }
        public double Amount { get; set; }

        [ForeignKey(typeof(Food))]
        public int FoodId { get; set; }

        [ForeignKey(typeof(Recipe))]
        public int RecipeId { get; set; }
    }

    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Name { get; set; }
        public bool IsReadOnly { get; set; }
    }

    public class RecipeItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Recipe))]
        public int RecipeId { get; set; }

        [ForeignKey(typeof(Food))]
        public int FoodId { get; set; }

        public double Amount { get; set; }
    }

    public class Food
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        //Size of one portion, in gr or ml
        public int Portion { get; set; }
        public int PortionType { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double SaturatedFat { get; set; }
        public double Carbs { get; set; }
        public double Sugar { get; set; }
        public double Fiber { get; set; }
        public double Natrium { get; set; }
        public string BarCode { get; set; }
    }
}
