using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FoodLog.FoodData;
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

        public LogAddViewModel(IFoodRepository foodRepository, FoodPerDay entry)
        {
            FoodRepository = foodRepository;
            Item = entry;

            SelectableFoods = new ObservableCollection<Food>();

            MealTypes = new ObservableCollection<MealTypeItem>();
            MealTypes.Add(new MealTypeItem() { MealType = MealType.BreakFast, Name = "Ontbijt" });
            MealTypes.Add(new MealTypeItem() { MealType = MealType.Lunch, Name = "Lunch" });
            MealTypes.Add(new MealTypeItem() { MealType = MealType.Diner, Name = "Diner" });
            MealTypes.Add(new MealTypeItem() { MealType = MealType.Snack, Name = "Tussendoor" });

            SelectedMealType = MealTypes.First(f => f.MealType == entry.MealType);
        }

        internal async Task SearchTextChanged(string newTextValue)
        {
            var r = await FoodRepository.FindFood(newTextValue);
            SelectableFoods.Clear();
            r.ForEach(i => SelectableFoods.Add(i));
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>(async (text) =>
                {
                    await SearchTextChanged(text);
                }));
            }
        }
    }
}
