using System.ComponentModel;
using Xamarin.Forms;
using Recipes.ViewModels;
using System;
using System.Linq;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Essentials;
using Recipes.Models;

namespace Recipes.Views
{
    public partial class HitDetailPage : ContentPage
    {

        HitDetailViewModel _viewModel;

        public HitDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new HitDetailViewModel();
        }
    }
}