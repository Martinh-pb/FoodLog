using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using FoodLog.FoodData;
using FoodLog.FoodModels;
using FoodLog.Views.Food;
using Xamarin.Forms;

namespace FoodLog.ViewModels
{
    public class FoodOverviewViewModel : BaseViewModel
    {
        public ObservableCollection<Food> Foods { get; set; }
        public string SearchText {get; set;}
        public Command LoadItemsCommand { get; set; }

        public FoodOverviewViewModel(IFoodRepository foodRepository)
        {
            FoodRepository = foodRepository;

            Foods = new ObservableCollection<Food>();

            LoadItemsCommand = new Command(async () => await ExecuteDayCommand());

            MessagingCenter.Subscribe<FoodAddPage, Food>(this, "SaveItem", async (obj, item) =>
            {
                await SaveFood(item as Food);
            });

            MessagingCenter.Subscribe<FoodDetailPage, Food>(this, "SaveItem", async (obj, item) =>
            {
                await SaveFood(item as Food);
            });
        }

        private async Task<int> SaveFood(Food food)
        {
            return await FoodRepository.SaveFood(food);
        }

        internal async Task SearchTextChanged(string newTextValue)
        {
            if (string.IsNullOrEmpty(newTextValue))
            {
                await ExecuteDayCommand();
            }
            else
            {
                var r = await FoodRepository.FindFood(SearchText);
                Foods.Clear();
                r.ForEach(i => Foods.Add(i));
                OnPropertyChanged("Foods");
            }
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>(async (text) =>
                {
                    await SearchTextChanged(text);
                    /*
                    var r =  await FoodRepository.FindFood(SearchText);
                    Foods.Clear();
                    r.ForEach(i => Foods.Add(i));
                    OnPropertyChanged("Foods");
                    */                   
                }));
            }
        }

        async Task ExecuteDayCommand()
        {
            if (DesignMode.IsDesignModeEnabled)
            {
                return;
            }

            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Foods.Clear();
                System.Collections.Generic.List<Food> r = await FoodRepository.GetFoods();
                r.ForEach(i => Foods.Add(i));

                OnPropertyChanged("Foods");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
