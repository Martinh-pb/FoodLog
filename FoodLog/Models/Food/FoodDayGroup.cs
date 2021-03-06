﻿using System;
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

        private bool showPercentage;
        public bool ShowPercentage
        {
            get { return showPercentage; }
            set
            {
                showPercentage = value;
            }
        }

        private string calculated;
        public string Calculated
        {
            get
            {
                return calculated;
            }
            set
            {
                calculated = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Calculated"));
            }
        }

        public string Calcutate() 
        {
            if (ShowPercentage)
            {
                return string.Format("c {0}% - e {1}% - v {2}%",
                        CarbInPercent,
                        ProteinInPercent,
                        FatInPercent);
            }
            else
            {
                return string.Format("c {0} - e {1} - v {2}",
                        Carb,
                        Protein,
                        Fat);
            }
        }

        private double calcCarbs;
        public double CalcCarbs
        {
            get { return calcCarbs; }
            set
            {
                calcCarbs = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CalcCarbs"));
            }
        }

        public double Calories 
        { 
            get 
            {
                return Items.Sum(s => s.Calories);
            }
        }

        public double Carb
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
                return CalculatePercentage(Carb);
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
            double total = Carb + Fat + Protein;

            if (Math.Abs(total) < 1)
                return 0;

            return Math.Round((toCalc / total) * 100);
        }

        public FoodDayGroup(string title, MealType mealType)
        {
            Title = title;
            MealType = mealType;
            ShowPercentage = true;
        }
    }
}
