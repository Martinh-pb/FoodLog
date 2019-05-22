using System;
using System.Collections.Generic;
using FoodLog.FoodModels;
using FoodLog.ViewModels.LogFood;
using Xamarin.Forms;

namespace FoodLog.Views.LogFood
{
    public partial class LogAddPage : ContentPage
    {
        LogAddViewModel viewModel;

        public LogAddPage(LogAddViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }

        public LogAddPage()
        {
            InitializeComponent();

            FoodPerDay entry = new FoodPerDay();

            this.viewModel = new LogAddViewModel(entry);
            BindingContext = this.viewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FoodModels.Food;
            if (item == null)
                return;

            FoodPerDay entry = viewModel.Item;
            entry.Food = item;
            entry.Food.Id = item.Id;

            await Navigation.PushAsync(new LogAddToDayPage(new LogAddToDayModel(entry)));

            // Manually deselect item.
            FoodListView.SelectedItem = null;
        }


        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", this.viewModel.Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
