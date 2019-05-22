using System;
using Xamarin.Forms;
using FoodLog.Views;
using FoodLog.Data;
using System.IO;

namespace FoodLog
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure

        static FoodDatabase database;
        static IFoodRepository repository;

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

        private static FoodDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FoodDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FoodLogDB.db3"));
                }

                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            
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
