using System;
using System.Collections.Generic;
using FoodLog.RecipeData;
using FoodLog.ViewModels.Recipe;
using Xamarin.Forms;

namespace FoodLog.Views.Recipe
{
    public partial class RecipeOverviewPage : ContentPage
    {
        public RecipeOverviewViewModel viewModel;

        public RecipeOverviewPage()
        {
            InitializeComponent();

            var repo = Injector.Resolve<IRecipeRepository>();
            BindingContext = viewModel = new RecipeOverviewViewModel(repo);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Models.Recipe.Recipe;
            if (item == null)
                return;

            //await Navigation.PushAsync(new LogDetailPage(new LogDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            Models.Recipe.Recipe entry = new Models.Recipe.Recipe();


            //await Navigation.PushModalAsync(new NavigationPage(new LogAddPage(new LogAddViewModel(entry))));
        }

        async public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Models.Recipe.Recipe toDelete = mi.CommandParameter as Models.Recipe.Recipe;

            await viewModel.DeleteItem(toDelete);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!viewModel.Initialized)
                viewModel.InitCommand.Execute(null);
        }
    }
}
