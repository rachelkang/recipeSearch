using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Recipes.Services;
using Recipes.Views;
using System.Collections.Generic;

namespace Recipes
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Xamarin.Forms.Device.SetFlags(new List<string> { "AccessibilityExperimental" });
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
