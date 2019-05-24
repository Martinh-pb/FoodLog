using System;
using Xamarin.Forms;
using FoodLog.Views;
using FoodLog.FoodData;
using System.IO;
using FoodLog.RecipeData;

namespace FoodLog
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        /*
        static FoodDatabase database;
        static IFoodRepository repository;
        static IRecipeRepository recipeRepository;
        */

        public static string DBPath { get; private set; } 
        /*
        public static IFoodRepository Repository
        { 
            get
            {
                if (repository == null)
                {
                    repository = new FoodRepository(Database);
                }

                return repository;
            }
        }

        public static IRecipeRepository RecipeRepository
        {
            get
            {
                if (recipeRepository == null)
                {
                    recipeRepository = new RecipeRepository(Database);
                }

                return recipeRepository;
            }
        }

        private static FoodDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FoodDatabase();
                }

                return database;
            }
        }
        */

        public App()
        {
            InitializeComponent();

            DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FoodLogDB.db3");

            Injector.RegisterType<IFoodDatabase, FoodDatabase>();
            Injector.RegisterType<IRecipeRepository, RecipeRepository>();
            Injector.RegisterType<IFoodRepository, FoodRepository>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
