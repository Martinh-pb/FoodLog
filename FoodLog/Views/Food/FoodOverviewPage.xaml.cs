using System;
using System.Collections.Generic;
using FoodLog.FoodData;
using FoodLog.ViewModels;
using Xamarin.Forms;

namespace FoodLog.Views.Food
{
    public partial class FoodOverviewPage : ContentPage
    {
        FoodOverviewViewModel viewModel;

        public FoodOverviewPage()
        {
            InitializeComponent();

            var repo = Injector.Resolve<IFoodRepository>();
            BindingContext = viewModel = new FoodOverviewViewModel(repo);
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new FoodAddPage()));
        }

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            FoodModels.Food f = e.SelectedItem as FoodModels.Food;
            if (f == null)
                return;

            await Navigation.PushAsync(new FoodDetailPage(f));

            // Manually deselect item.
            FoodListView.SelectedItem = null;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var scan = new ZXing.Net.Mobile.Forms.ZXingScannerPage();

            scan.OnScanResult += (result) => {
                scan.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () => {
                    await Navigation.PopAsync();
                    await DisplayAlert("result", result.Text, "Cancel");
                    ;
                });
            };

            await Navigation.PushAsync(scan);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Foods.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
            //viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
