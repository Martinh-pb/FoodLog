using System;
using System.Collections.Generic;
using FoodLog.FoodModels;
using FoodLog.ViewModels.LogFood;
using Xamarin.Forms;

namespace FoodLog.Views.LogFood
{
    public partial class LogDetailPage : ContentPage
    {
        LogDetailViewModel viewModel;

        public LogDetailPage(LogDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public LogDetailPage()
        {
            InitializeComponent();

            var item = new FoodPerDay
            {

                MealType = (int)MealType.BreakFast
            };

            viewModel = new LogDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}
