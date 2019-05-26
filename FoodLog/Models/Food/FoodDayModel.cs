using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public class FoodPerDay : INotifyPropertyChanged, ICloneable
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public MealType MealType { get; set; }
        public Food Food { get; set; }

        public FoodPerDay()
        {
            Macros = new Macros();
        }

        public void Calculate()
        {
            if (Food != null)
            {
                Macros = Food.GetMacros(_amount);
            }
            else
            {
                Macros = new Macros();
            }
        }

        private double _amount;
        public double Amount
        {
            get 
            { 
                return _amount; 
            }
            set
            {
                _amount = value;

                Calculate();

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Calories"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Carb"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Fat"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Protein"));
            }
        }

        private Macros Macros
        {
            get;set;
        }


        public string AmountDescription
        {
            get
            {
                if (Food == null)
                    return string.Empty;

                return Food.GetPortionDescription(Amount);
            }
        }

        public double Calories
        {
            get
            {
                return Macros.Calories;
            }
        }

        public double Carb
        {
            get
            {
                return Macros.Carbs;
            }
        }

        public double Protein
        {
            get
            {
                return Macros.Protein;
            }
        }

        public double Fat
        {
            get
            {
                return Macros.Fat;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public object Clone()
        {
            var f = new FoodPerDay();
            f.Id = Id;
            f.Amount = Amount;
            f.Date = Date;
            f.Time = Time;
            f.MealType = MealType;
            f.Food = Food.Clone() as Food;
            f.Macros = Macros.Clone() as Macros;

            return f;
        }
    }

    public class Macros : ICloneable
    { 
        public double Calories { get; set; }
        public double Carbs { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; }

        public object Clone()
        {
            var c = new Macros();
            c.Calories = Calories;
            c.Carbs = Carbs;
            c.Fat = Fat;
            c.Protein = Protein;

            return c;
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

        public double Calc(double item, double Amount)
        {
            return (item / Portion) * Amount;
        }

        public Macros GetMacros(double amount)
        {
            Macros m = new Macros();
            m.Calories = Calc(Calories, amount);
            m.Carbs = Calc(Carbs, amount);
            m.Fat = Calc(Fat, amount);
            m.Protein = Calc(Protein, amount);

            return m;
        }

        public string GetPortionDescription(double amount)
        {
            string desc = PortionType == PortionType.Gram ? "gram" : "ml";
            return string.Format("{0} {1}", amount, desc);
        }

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
