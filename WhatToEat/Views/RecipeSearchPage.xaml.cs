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
        ItemsViewModel _viewModel;

        public RecipeSearchPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemsViewModel();
            _restService = new RestService();
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(searchEntry.Text))
            {
                var recipeDataTask = _restService.GetRecipeDataAsync(GenerateRequestUri(Constants.EdamamEndpoint));
                recipeDataTask.Wait();
                RecipeData recipeData = recipeDataTask.Result;
                BindingContext = recipeData;
            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={searchEntry.Text}";
            requestUri += $"&app_id={Constants.EdamamAppId}";
            requestUri += $"&app_key={Constants.EdamamAppKey}";
            //requestUri += $"&from=0&to=1";
            return requestUri;
        }
    }
}