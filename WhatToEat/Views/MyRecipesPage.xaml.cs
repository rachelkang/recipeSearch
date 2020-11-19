using Xamarin.Forms;
using Recipes.ViewModels;

namespace Recipes.Views
{
    public partial class MyRecipesPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public MyRecipesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}