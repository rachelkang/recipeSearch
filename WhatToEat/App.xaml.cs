using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Recipes.Services;
using Recipes.Views;
using System.Collections.Generic;
using Microsoft.Maui.Graphics;

namespace Recipes
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
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
