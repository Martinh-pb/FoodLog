using System;
using System.Collections.Generic;
using FoodLog.FoodModels;
using Xamarin.Forms;

namespace FoodLog.Views.Food
{
    public class PortionTypeItem
    {
        public PortionType PortionType { get; set; }
        public string Name { get; set; }

        public static List<PortionTypeItem> PortionTypeItems()
        {
            var portionItems = new List<PortionTypeItem>();
            portionItems.Add(new PortionTypeItem() { PortionType = PortionType.Gram, Name = "Gram" });
            portionItems.Add(new PortionTypeItem() { PortionType = PortionType.Ml, Name = "ml" });

            return portionItems;
        }
    }

    public partial class FoodAddPage : ContentPage
    {
        public FoodAddPage()
        {
            Food = new FoodModels.Food()
            {
                Name = "",
                Calories = 0,
                Fat = 0,
                Carbs = 0,
                Fiber = 0,
                Natrium = 0,
                Portion = 100,
                PortionType = 0,
                Protein = 0,
                SaturatedFat = 0,
                Sugar = 0
            };

            PortionItems = PortionTypeItem.PortionTypeItems();

            SelectedPortion = PortionItems[0];

            InitializeComponent();
            BarCode.Text = Food.BarCode;

            BindingContext = this;
        }
        public FoodModels.Food Food { get; set; }

        public List<PortionTypeItem> PortionItems { get; set; }
        public PortionTypeItem SelectedPortion { get; set; }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Food.PortionType = SelectedPortion.PortionType;
            Food.BarCode = BarCode.Text;

            MessagingCenter.Send(this, "SaveItem", Food);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
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
