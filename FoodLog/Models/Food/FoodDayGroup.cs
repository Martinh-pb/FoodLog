using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FoodLog.FoodModels
{
    public class FoodDayGroup : ObservableCollection<FoodPerDay>
    {
        public string Title { get; set; }
        public MealType MealType { get; set; }

        public FoodDayGroup(string title, MealType mealType)
        {
            Title = title;
            MealType = mealType;
            ShowPercentage = true;
        }

        private bool showPercentage;
        public bool ShowPercentage
        {
            get { return showPercentage; }
            set
            {
                showPercentage = value;
                Calcutate();
            }
        }

        public void Calcutate() 
        {
            string perc = ShowPercentage ? "%" : string.Empty;

            if (ShowPercentage)
            {
                CarbDescrip = $"Carbs {Math.Round(CarbInPercent, 1, MidpointRounding.ToEven):N2}%";
                ProteinDescrip =$"Prot {Math.Round(ProteinInPercent, 1, MidpointRounding.ToEven):N2}%";
                FatDescrip = $"Fat {Math.Round(FatInPercent, 1, MidpointRounding.ToEven):N2}%";
                CaloriesDescrip = $"{Math.Round(Calories,1, MidpointRounding.ToEven):N2}";
            }
            else
            {
                CarbDescrip = $"Carbs {Math.Round(Carbs, 1, MidpointRounding.ToEven):N2}";
                ProteinDescrip = $"Prot {Math.Round(Protein, 1, MidpointRounding.ToEven):N2}";
                FatDescrip = $"Fat {Math.Round(Fat, 1, MidpointRounding.ToEven):N2}";
                CaloriesDescrip = $"{Math.Round(Calories, 1, MidpointRounding.ToEven):N2}";
            }
        }

        private string _carbDescrip;
        public string CarbDescrip
        {
            get
            {
                return _carbDescrip;
            }
            set
            {
                _carbDescrip = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CarbDescrip"));
            }
        }

        private string _proteinDescrip;
        public string ProteinDescrip
        {
            get
            {
                return _proteinDescrip;
            }
            set
            {
                _proteinDescrip = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ProteinDescrip"));
            }
        }

        private string _fatDescrip;
        public string FatDescrip
        {
            get
            {
                return _fatDescrip;
            }
            set
            {
                _fatDescrip = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FatDescrip"));
            }
        }

        private string _caloriesDescrip;
        public string CaloriesDescrip
        {
            get
            {
                return _caloriesDescrip;
            }
            set
            {
                _caloriesDescrip = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CaloriesDescrip"));
            }
        }

        public double Calories 
        { 
            get 
            {
                return Items.Sum(s => s.Calories);
            }
        }

        public double Carbs
        {
            get
            {
                return Items.Sum(s => s.Carb);
            }
        }

        public double CarbInPercent
        {
            get
            {
                return CalculatePercentage(Carbs);
            }
        }

        public double Protein
        {
            get
            {
                return Items.Sum(s => s.Protein);
            }
        }

        public double ProteinInPercent
        {
            get
            {
                return CalculatePercentage(Protein);
            }
        }

        public double Fat
        {
            get
            {
                return Items.Sum(s => s.Fat);
            }
        }

        public double FatInPercent
        {
            get
            {
                return CalculatePercentage(Fat);
            }
        }

        private double CalculatePercentage(double toCalc)
        {
            double total = Carbs + Fat + Protein;

            if (Math.Abs(total) < 1)
                return 0;

            return (toCalc / total) * 100;
        }

    }
}
