using Recipes.ViewModels;

namespace Recipes.Views
{
    public partial class EditRecipePage : ContentPage
    {
        EditRecipeViewModel _viewModel;

        public EditRecipePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new EditRecipeViewModel();
        }
    }
}