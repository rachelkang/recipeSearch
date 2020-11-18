using System;
using System.ComponentModel;
using Recipes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recipes.Views
{
    public partial class RecipeSearchPage : ContentPage
    {
        RecipeSearchViewModel _viewModel;

        public RecipeSearchPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new RecipeSearchViewModel();
        }

    }
}