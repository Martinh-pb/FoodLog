using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodLog.FoodModels
{
    public enum MealType
    {
        BreakFast = 0,
        Lunch = 1,
        Diner = 2,
        Snack = 3,
        All = 100
    }

    public enum PortionType
    {
        Gram = 0,
        Ml = 1
    }

    public class FoodPerDay : ICloneable
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public MealType MealType { get; set; }
        public double Amount { get; set; }
        public Food Food { get; set; }

        public double Calories
        {
            get
            {
                return Calc(Food.Calories, Food.Portion, Amount);
            }
        }

        public double Carb
        {
            get
            {
                return Calc(Food.Carbs, Food.Portion, Amount);
            }
        }

        public double Protein
        {
            get
            {
                return Calc(Food.Protein, Food.Portion, Amount);
            }
        }

        public double Fat
        {
            get
            {
                return Calc(Food.Fat, Food.Portion, Amount);
            }
        }

        public static double Calc(double item, double portion, double Amount)
        {
            return (item / portion) * Amount;
        }

        public object Clone()
        {
            var f = new FoodPerDay();
            f.Id = Id;
            f.Amount = Amount;
            f.Date = Date;
            f.Time = Time;
            f.MealType = MealType;
            f.Food = Food.Clone() as Food;

            return f;
        }
    }

    public class Food : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //Size of one portion, in gr or ml
        public int Portion { get; set; }
        public PortionType PortionType { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double SaturatedFat { get; set; }
        public double Carbs { get; set; }
        public double Sugar { get; set; }
        public double Fiber { get; set; }
        public double Natrium { get; set; }
        public string BarCode { get; set; }

        public object Clone()
        {
            var f = new Food();
            f.Id = Id;
            f.Name = Name;
            f.Portion = Portion;
            f.PortionType = PortionType;
            f.Calories = Calories;
            f.Protein = Protein;
            f.Fat = Fat;
            f.SaturatedFat = SaturatedFat;
            f.Carbs = Carbs;
            f.Sugar = Sugar;
            f.Fiber = Fiber;
            f.Natrium = Natrium;
            f.BarCode = BarCode;

            return f;
        }
    }
}
