using System.ComponentModel;
using Xamarin.Forms;
using Recipes.ViewModels;
using System;

namespace Recipes.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}