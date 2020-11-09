using System;
using System.ComponentModel;
using System.Reflection;
using Recipes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recipes.Views
{
    public partial class StartingPage : ContentPage
    {
        StartingPageViewModel _viewModel;

        public StartingPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new StartingPageViewModel();
            //LoadImages();
        }

        //private void LoadImages()
        //{
        //    Image balancedMealsImage = new Image
        //    {
        //        Source = ImageSource.FromResource("Resources.fork-knife.png", typeof(StartingPage).GetTypeInfo().Assembly)
        //    };

        //    balancedMeals.Children.Insert(0, balancedMealsImage);
        //}
    }
}