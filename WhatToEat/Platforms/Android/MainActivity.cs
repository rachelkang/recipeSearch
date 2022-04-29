using Android.App;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;

namespace Recipes
{
	[IntentFilter(
		new[] { Platform.Intent.ActionAppAction },
		Categories = new[] { Android.Content.Intent.CategoryDefault })]
	[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true)]
	public class MainActivity : MauiAppCompatActivity
	{

		protected override void OnResume()
		{
			base.OnResume();

			Platform.OnResume(this);
		}

		protected override void OnNewIntent(Android.Content.Intent intent)
		{
			base.OnNewIntent(intent);

			Platform.OnNewIntent(intent);
		}
	}
}