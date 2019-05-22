using System;
using System.Collections.Generic;
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

            BindingContext = viewModel = new LogDayViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FoodPerDay;
            if (item == null)
                return;

            await Navigation.PushAsync(new LogDetailPage(new LogDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            FoodPerDay entry = new FoodPerDay()
            {
                Date = viewModel.Date,
                Time = viewModel.Date
            };

            await Navigation.PushModalAsync(new NavigationPage(new LogAddPage(new LogAddViewModel(entry))));
        }

        async public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            FoodPerDay toDelete = mi.CommandParameter as FoodPerDay;

            await viewModel.DeleteItem(toDelete);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Date == DateTime.MinValue)
                viewModel.InitDayCommand.Execute(null);
        }
    }
}
