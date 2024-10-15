using ShopNow.Services;
using ShopNow;
using System.IO;
using System;
using Microsoft.Maui.Controls;
using ShopNow.Views;


namespace ShopNow
{
    public partial class App : Application
    {
        public static string DatabasePath { get; private set; }

        private static DatabaseService _database;

        public App(string dbPath)
        {
            InitializeComponent();
            SQLitePCL.Batteries_V2.Init();
            DatabasePath = dbPath;

            // Set the SplashPage as the first page of the app
            MainPage = new NavigationPage(new SplashPage());

        }


        // This method is called when the app starts (used to seed database)
        protected override async void OnStart()
        {
            // Call the SeedDatabaseAsync method to seed the categories
            await Database.SeedDatabaseAsync();
        }



        // Initialize or return the existing DatabaseService
        public static DatabaseService Database
        {
            get
            {
                if (_database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShopNow.db3");
                    _database = new DatabaseService(dbPath);
                }
                return _database;
            }
        }
    }
}