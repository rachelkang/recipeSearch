using Recipes.ViewModels;

namespace Recipes.Views
{
    public partial class EditRecipePage : ContentPage
    {
        public EditRecipePage()
        {
            InitializeComponent();
            BindingContext = new EditRecipeViewModel();
        }

        private void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            SemanticScreenReader.Announce(SemanticProperties.GetDescription(ratingLabel));
        }
    }
}