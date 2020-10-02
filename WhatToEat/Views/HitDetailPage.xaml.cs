using System.ComponentModel;
using Xamarin.Forms;
using Recipes.ViewModels;
using System;
using System.Linq;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Essentials;

namespace Recipes.Views
{
    [QueryProperty(nameof(HitId), nameof(HitId))]
    public partial class HitDetailPage : ContentPage
    {
        // Launcher.OpenAsync is provided by Xamarin.Essentials.
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public HitDetailPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public Hit Hit { get; set; }

        private string hitId;

        public string HitId
        {
            get
            {
                return hitId;
            }
            set
            {
                hitId = value;
                LoadItemId(value);
            }
        }

        public void LoadItemId(string hitId)
        {
            try
            {
                Hit = AppShell.Data.Hits.FirstOrDefault(h => h.Id == int.Parse(hitId));
                OnPropertyChanged(nameof(Hit));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Hit");
            }
        }
    }
}