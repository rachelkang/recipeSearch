using Recipes.Models;
using Recipes.ViewModels;

namespace Recipes.Views
{
    public partial class NewRecipePage : ContentPage
    {
        public Item Item { get; set; }

        public NewRecipePage()
        {
            InitializeComponent();
            BindingContext = new NewRecipeViewModel();
        }

        private async void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
			await Task.Delay(100);
			SemanticScreenReader.Announce(SemanticProperties.GetDescription(ratingLabel));
        }
    }
}