using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Recipes.ViewModels;
using System.Linq;

namespace Recipes.Views
{
    public partial class RecipeSearchPage : ContentPage
    {
        RecipeSearchViewModel _viewModel;

        public RecipeSearchPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new RecipeSearchViewModel();

	}


        bool _firstNavigatedTo = true;
        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            if (_firstNavigatedTo)
            {
                _firstNavigatedTo = false;
                await Task.Delay(200); // try without this
                // Set semantic focus to the search box
            }
        }
		private void OnImageHandlerChanged(object sender, System.EventArgs e)
		{
#if ANDROID
			if (sender is IView view)
			{
				if (view.Handler?.PlatformView is Android.Widget.ImageView aView)
				{

				}
				else if(view.Handler?.PlatformView is Android.Views.ViewGroup vg)
				{
					vg.SetClipChildren(true);
				}
			}
#endif
		}

		void RecipeSearchPage_Tapped(object sender, System.EventArgs e)
		{
			BindableObject bo = sender as BindableObject;
			_viewModel.ItemTapped.Execute(bo.BindingContext);

		}

		//private void RecipeSearchPage_SelectedItemsChanged(object sender, Microsoft.Maui.SelectedItemsChangedEventArgs e)
		//{
		//	foreach(var item in e.NewSelection)
		//	{
		//		vListView.SetDeselected(vListView.SelectedItems.ToArray());
		//		_viewModel.SelectedHit = _viewModel.RecipeData.Hits[item.ItemIndex];
		//	}

		//}

		protected override void OnAppearing()
        {
			_viewModel.SelectedHit = null;
			base.OnAppearing();
            _viewModel.SearchCommand.Execute(null);
        }
    }
}
