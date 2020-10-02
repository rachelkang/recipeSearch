using System.ComponentModel;
using Xamarin.Forms;
using Recipes.ViewModels;
using System;

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
    }
}