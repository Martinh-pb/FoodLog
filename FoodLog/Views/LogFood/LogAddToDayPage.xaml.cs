using System;
using System.Collections.Generic;
using FoodLog.FoodModels;
using FoodLog.ViewModels.LogFood;
using Xamarin.Forms;

namespace FoodLog.Views.LogFood
{
    public partial class LogAddToDayPage : ContentPage
    {
        LogAddToDayModel viewModel;

        public LogAddToDayPage(LogAddToDayModel model)
        {
            InitializeComponent();

            viewModel = model;
            BindingContext = viewModel;
        }

        public LogAddToDayPage()
        {
            InitializeComponent();
            FoodPerDay entry = new FoodPerDay();
            entry.Food = new FoodModels.Food() { Name = "Paprika" };
            LogAddToDayModel model = new LogAddToDayModel(entry);

            viewModel = model;
            BindingContext = viewModel;
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            viewModel.CalcMacros();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", this.viewModel.FoodEntry);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
