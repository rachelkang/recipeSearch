using Recipes.ViewModels;

namespace Recipes.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
		BindingContext = new AboutViewModel();
	}
}
