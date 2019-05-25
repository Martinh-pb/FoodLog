using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodLog.FoodData;
using FoodLog.FoodModels;
using FoodLog.ViewModels.LogFood;
using Xamarin.Forms;

namespace FoodLog.Views.LogFood
{
    public partial class LogOverviewPage : ContentPage
    {
        LogDayViewModel viewModel;

        public LogOverviewPage()
        {
            InitializeComponent();

            if (DesignMode.IsDesignModeEnabled)
            {
                viewModel = ViewModelLocator.ViewModelLocator.ItemsViewModel;
            }
            else
            {
                var r = Injector.Resolve<IFoodRepository>();
                viewModel = new LogDayViewModel(r);

            }
            BindingContext = viewModel;

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FoodPerDay;
            if (item == null)
                return;

            if (item.Id < 0)
            {
                await AddItem(item.MealType);
            }
            else
            {
                await Navigation.PushAsync(new LogDetailPage(new LogDetailViewModel(item)));
            }
            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await AddItem(MealType.BreakFast);
        }

        async Task AddItem(MealType mealType)
        {
            FoodPerDay entry = new FoodPerDay()
            {
                MealType = mealType,
                Date = viewModel.Date,
                Time = viewModel.Date
            };

            var repo = Injector.Resolve<IFoodRepository>();
            await Navigation.PushModalAsync(new NavigationPage(new LogAddPage(new LogAddViewModel(repo, entry))));
        }

        async public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            FoodPerDay toDelete = mi.CommandParameter as FoodPerDay;

            await viewModel.DeleteItem(toDelete);
        }

        public void ListHeaderTapped(object sender, EventArgs e)
        {
            StackLayout stackLayout = sender as StackLayout;
            FoodDayGroup g = stackLayout.BindingContext as FoodDayGroup;
            viewModel.SwitchHeader(g);
        }

        public void Previous_Date_Clicked(object sender, EventArgs e)
        {
            viewModel.GotoPreviousDate();
        }

        public void Next_Date_Clicked(object sender, EventArgs e)
        {
            viewModel.GotoNextDate();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Date == DateTime.MinValue)
            {
                viewModel.InitDayCommand.Execute(null);
            }
        }
    }
}
