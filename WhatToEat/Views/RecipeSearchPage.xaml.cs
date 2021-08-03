using Microsoft.Maui.Controls;
using Recipes.ViewModels;

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.SearchCommand.Execute(null);
        }
    }
}