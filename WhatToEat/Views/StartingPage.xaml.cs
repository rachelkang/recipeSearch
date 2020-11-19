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
        }
    }
}