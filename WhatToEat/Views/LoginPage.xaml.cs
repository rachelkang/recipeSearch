using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Recipes.ViewModels;

namespace Recipes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}