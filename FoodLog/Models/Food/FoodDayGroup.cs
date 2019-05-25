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

            CarbDescrip = string.Format("Carb {0}{1}",
                    ShowPercentage ? CarbInPercent : Carb,
                    perc);

            ProteinDescrip = string.Format("Prot {0}{1}",
                    ShowPercentage ? ProteinInPercent : Protein,
                    perc);

            FatDescrip = string.Format("Fat {0}{1}",
                    ShowPercentage ? FatInPercent : Fat,
                    perc);

            CaloriesDescrip = string.Format("{0}",
                    Calories);

            if (ShowPercentage)
            {

            }
            else
            {
                /*
                return string.Format("c {0} - e {1} - v {2}",
                        Carb,
                        Protein,
                        Fat);
                        */                       
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

        private double Calories 
        { 
            get 
            {
                return Items.Sum(s => s.Calories);
            }
        }

        private double Carb
        {
            get
            {
                return Items.Sum(s => s.Carb);
            }
        }

        private double CarbInPercent
        {
            get
            {
                return CalculatePercentage(Carb);
            }
        }

        private double Protein
        {
            get
            {
                return Items.Sum(s => s.Protein);
            }
        }

        private double ProteinInPercent
        {
            get
            {
                return CalculatePercentage(Protein);
            }
        }

        private double Fat
        {
            get
            {
                return Items.Sum(s => s.Fat);
            }
        }

        private double FatInPercent
        {
            get
            {
                return CalculatePercentage(Fat);
            }
        }

        private double CalculatePercentage(double toCalc)
        {
            double total = Carb + Fat + Protein;

            if (Math.Abs(total) < 1)
                return 0;

            return Math.Round((toCalc / total) * 100);
        }

    }
}
