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

        private async void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
			await Task.Delay(100);
			SemanticScreenReader.Announce(SemanticProperties.GetDescription(ratingLabel));
        }
    }
}