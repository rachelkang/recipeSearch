using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Recipes.ViewModels;

namespace Recipes.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel _viewModel;

        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemDetailViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
		}

		async void OpenUrl(object sender, System.EventArgs e)
		{
            System.Diagnostics.Debug.WriteLine("WHAT");
			await Launcher.OpenAsync(_viewModel.RecipeUrl);

		}

        void Button_Loaded(System.Object sender, System.EventArgs _)
        {
#if IOS || MACCATALYST
            if (sender is IElement e && e.Handler.PlatformView is UIKit.UIView uiView)
            {
                uiView.AccessibilityTraits = UIKit.UIAccessibilityTrait.Link;
                System.Diagnostics.Debug.WriteLine($"{uiView.AccessibilityTraits}");
            }
            System.Diagnostics.Debug.WriteLine("asdf");
#endif
        }
    }
}