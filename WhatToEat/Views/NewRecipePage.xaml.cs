using Microsoft.Maui.Controls;
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
    }
}