using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FoodLog.Views.Food
{
    public partial class FoodDetailPage : ContentPage
    {
        public FoodModels.Food Food { get; set; }

        public List<PortionTypeItem> PortionItems { get; set; }
        public PortionTypeItem SelectedPortion { get; set; }

        public FoodDetailPage(FoodModels.Food food)
        {
            Food = food.Clone() as FoodLog.FoodModels.Food;
            InitializeComponent();

            PortionItems = PortionTypeItem.PortionTypeItems();

            SelectedPortion = PortionItems[0];
            BarCode.Text = Food.BarCode;

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Food.PortionType = SelectedPortion.PortionType;
            Food.BarCode = BarCode.Text;

            MessagingCenter.Send(this, "SaveItem", Food);
            await Navigation.PopAsync();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var scan = new ZXing.Net.Mobile.Forms.ZXingScannerPage();

            scan.OnScanResult += (result) => {
                scan.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () => {
                    await Navigation.PopAsync();
                    BarCode.Text = result.Text;

                });
            };

            await Navigation.PushAsync(scan);
        }
    }
}
