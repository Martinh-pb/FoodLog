using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FoodLog.RecipeData;
using Xamarin.Forms;

namespace FoodLog.ViewModels.Recipe
{
    public class RecipeOverviewViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Recipe.Recipe> Recipes { get; set; }
        public bool Initialized { get; set; }

        public Command InitCommand { get; set; }

        public RecipeOverviewViewModel(IRecipeRepository recipeRepository)
        {
            RecipeRepository = recipeRepository;
            Recipes = new ObservableCollection<Models.Recipe.Recipe>();

            InitCommand = new Command(async () => await ExecuteInit());
        }

        public async Task DeleteItem(Models.Recipe.Recipe toDelete)
        { 
        }

        private async Task ExecuteInit()
        {
            List<Models.Recipe.Recipe> entries = await RecipeRepository.GetRecipes();
            Recipes.Clear();

            entries.ForEach(r => Recipes.Add(r));
            Initialized = true;
        }
    }
}
