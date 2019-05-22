using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FoodLog.FoodModels;
using Xamarin.Forms;

namespace FoodLog.ViewModels.LogFood
{
    public class LogAddViewModel : BaseViewModel
    {
        public FoodPerDay Item { get; set; }
        public string SearchText { get; set; }

        public class MealTypeItem
        {
            public MealType MealType { get; set; }
            public string Name { get; set; }
        }

        public ObservableCollection<MealTypeItem> MealTypes { get; set; }
        public MealTypeItem SelectedMealType { get; set; }

        public ObservableCollection<Food> SelectableFoods { get; set; }

        public LogAddViewModel(FoodPerDay entry)
        {
            Item = entry;

            SelectableFoods = new ObservableCollection<Food>();

            MealTypes = new ObservableCollection<MealTypeItem>();
            MealTypes.Add(new MealTypeItem() { MealType = MealType.BreakFast, Name = "Ontbijt" });
            MealTypes.Add(new MealTypeItem() { MealType = MealType.Lunch, Name = "Lunch" });
            MealTypes.Add(new MealTypeItem() { MealType = MealType.Diner, Name = "Diner" });
            MealTypes.Add(new MealTypeItem() { MealType = MealType.Snack, Name = "Tussendoor" });

            SelectedMealType = MealTypes.First(f => f.MealType == entry.MealType);
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>(async (text) =>
                {
                    var r = await FoodDatabase.FindFood(SearchText);
                    SelectableFoods.Clear();
                    r.ForEach(i => SelectableFoods.Add(i));
                }));
            }
        }
    }
}
