using Xamarin.Forms;

using Recipes.Models;
using Recipes.ViewModels;

namespace Recipes.Views
{
    public partial class EditItemPage : ContentPage
    {
        public Item Item { get; set; }

        public EditItemPage()
        {
            InitializeComponent();
            BindingContext = new EditItemViewModel();
        }
    }
}