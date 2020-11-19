using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Recipes.ViewModels;
using Color = Xamarin.Forms.Color;

namespace Recipes.Views
{
    public partial class HitDetailPage : ContentPage
    {

        HitDetailViewModel _viewModel;
		Color primary = Color.FromHex("#2BB0ED");
		string alertMessage = "This recipe has been added to your personal recipe collection! Go to the 'My Recipes' tab to view and modify this recipe from there :)";

		public HitDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new HitDetailViewModel();
        }

		async void AddItem_Clicked(System.Object sender, System.EventArgs e)
		{
			var messageOptions = new MessageOptions
			{
				Foreground = primary,
				FontSize = 7,
				Message = alertMessage
			};

			var actionOptions = new List<SnackBarActionOptions>
			{
				new SnackBarActionOptions
				{
					ForegroundColor = Color.White,
					BackgroundColor = primary,
					FontSize = 7
				}
			};
			var options = new SnackBarOptions(messageOptions, 5000, primary, false, actionOptions);
			await this.DisplaySnackBarAsync(options);
		}
	}
}