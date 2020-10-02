using System;
using System.ComponentModel;
using Recipes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recipes.Views
{
    public partial class RecipeSearchPage : ContentPage
    {
        RestService _restService;

        public RecipeSearchPage()
        {
            InitializeComponent();
            _restService = new RestService();
            ButtonText = "Find Recipe";
            SearchQuery = "Macaroni";
            BindingContext = this;

            ItemTapped = new Command<Hit>(OnItemSelected);
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(searchEntry.Text))
            {
                RecipeData recipeData = await _restService.GetRecipeDataAsync(GenerateRequestUri(Constants.EdamamEndpoint));

                if (recipeData.Hits.Length == 0 || recipeData == null)
                {
                    NoResultsLabel.Text = $"Sorry - we couldn't find any recipes for {searchEntry.Text} :(";
                    NoResultsLabel.IsVisible = true;
                }
                else
                {
                    NoResultsLabel.IsVisible = false;
                }

                for (int i = 0; i < recipeData.Hits.Length; i++)
                {
                    recipeData.Hits[i].Id = i;
                }

                RecipeData = recipeData;
                AppShell.Data = RecipeData;
                OnPropertyChanged(nameof(RecipeData)); // tells Xaml view to update
                //BindingContext = recipeData;
            }
        }

        public RecipeData RecipeData { get; set; }

        public string ButtonText { get; set; }

        public string SearchQuery { get; set; }

        public string NoResults { get; set; }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={searchEntry.Text}";
            requestUri += $"&app_id={Constants.EdamamAppId}";
            requestUri += $"&app_key={Constants.EdamamAppKey}";
            //requestUri += $"&from=0&to=1";
            return requestUri;
        }

        public Command<Hit> ItemTapped { get; }

        async void OnItemSelected(Hit hit)
        {
            if (hit == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(HitDetailPage)}?HitId={hit.Id}");
        }
    }
}