using System;
using System.Diagnostics;
using System.Windows.Input;
using FoodLog.FoodModels;
using Xamarin.Forms;

namespace FoodLog.ViewModels.LogFood
{
    public class LogAddToDayModel : BaseViewModel
    {
        public FoodPerDay FoodEntry { get; set; }

        private double _cals;
        public double Cals
        {
            get { return _cals; }
            set { SetProperty(ref _cals, value); }
        }

        private double _carbs;
        public double Carbs
        {
            get { return _carbs; }
            set { SetProperty(ref _carbs, value); }
        }

        private double _carbsPerc;
        public double CarbsPerc
        {
            get { return _carbsPerc; }
            set { SetProperty(ref _carbsPerc, value); }
        }

        private double _protein;
        public double Protein
        {
            get { return _protein; }
            set { SetProperty(ref _protein, value); }
        }

        private double _proteinPerc;
        public double ProteinPerc
        {
            get { return _proteinPerc; }
            set { SetProperty(ref _proteinPerc, value); }
        }

        private double _fat;
        public double Fat
        {
            get { return _fat; }
            set { SetProperty(ref _fat, value); }
        }

        private double _fatPerc;
        public double FatPerc
        {
            get { return _fatPerc; }
            set { SetProperty(ref _fatPerc, value); }
        }

        private double _amount;
        public double Amount { get { return _amount; } set { SetProperty(ref _amount, value); } }

        public LogAddToDayModel(FoodPerDay foodEntry)
        {
            FoodEntry = foodEntry;
            FoodEntry.Amount = 100;
        }

        public void CalcMacros()
        {
            Cals = FoodEntry.Calories;
            Carbs = FoodEntry.Carb;
            Protein = FoodEntry.Protein;
            Fat = FoodEntry.Fat;

            CarbsPerc = CalculatePercentage(Carbs);
            ProteinPerc = CalculatePercentage(Protein);
            FatPerc = CalculatePercentage(Fat);
        }

        private double CalculatePercentage(double toCalc)
        {
            double total = Carbs + Fat + Protein;

            if (Math.Abs(total) < 1)
                return 0;

            return Math.Round((toCalc / total) * 100);
        }
    }
}
