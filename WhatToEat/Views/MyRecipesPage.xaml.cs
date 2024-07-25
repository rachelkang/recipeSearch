using Recipes.ViewModels;
#if WINDOWS
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System.Windows.Input;
#endif

namespace Recipes.Views
{
    public partial class MyRecipesPage : ContentPage
	{
		MyRecipesViewModel _viewModel;

        private double width = 0;
        private double height = 0;

        public MyRecipesPage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new MyRecipesViewModel();
#if WINDOWS
            vMyRecipesListView.Loaded += OnCarouselLoaded;
            vMyRecipesListView.Unloaded += OnCarouselUnloaded;
#endif
        }

#if WINDOWS
        void OnCarouselLoaded(object sender, EventArgs e)
        {
            if (vMyRecipesListView?.Handler?.PlatformView is UIElement element)
            {
                element.PreviewKeyDown += OnCarouselKeyDown;
            }


        }

        void OnCarouselUnloaded(object sender, EventArgs e)
        {
            if (vMyRecipesListView?.Handler?.PlatformView is UIElement element)
            {
                element.PreviewKeyDown -= OnCarouselKeyDown;
            }
        }

        void OnCarouselKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter || e.Key == Windows.System.VirtualKey.Space)
            {
                var currentItem = vMyRecipesListView.CurrentItem;
                if (currentItem is not null)
                {
                    _viewModel.ItemTapped.Execute(currentItem);
                }
                e.Handled = true;
            }
        }
#endif

        protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.OnAppearing();
		}

		private void MyRecipesPage_Tapped(object sender, System.EventArgs e)
		{
			BindableObject bo = sender as BindableObject;
			_viewModel.ItemTapped.Execute(bo.BindingContext);

		}

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    vMyRecipesListView.HeightRequest = 200;
                    vMyRecipesListView.WidthRequest = width - 100;
                }
                else
                {
                    vMyRecipesListView.HeightRequest = height - 150;
                    vMyRecipesListView.WidthRequest = 350;
                }
            }
        }
    }
}