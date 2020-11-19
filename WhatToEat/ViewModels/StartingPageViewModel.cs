using System;
using System.Globalization;
using System.Threading.Tasks;
using Recipes.Views;
using Xamarin.Forms;

namespace Recipes.ViewModels
{
    public class StartingPageViewModel : BaseViewModel
    {
        RestService _restService;

        string _searchQuery; // private by default, _name convention
        bool recipeTypeButtonsVisible;
        string noResultsLabel;
        bool noResultsVisible;

        public Command SearchCommand { get; }
        public Command<string> FilteredSearchCommand { get; }
        public Command<Hit> BalancedMealsTapped { get; }

        public StartingPageViewModel()
        {
            Title = "Recipes";
            _restService = new RestService();
            NoResultsVisible = false;
            RecipeTypeButtonsVisible = true;

            SearchCommand = new Command(async () => await OnSearch());
            FilteredSearchCommand = new Command<string>(async (filter) => await OnSearch(filter));
        }

        public bool RecipeTypeButtonsVisible
        {
            get => recipeTypeButtonsVisible;
            set => SetProperty(ref recipeTypeButtonsVisible, value);
        }

        public RecipeData RecipeData { get; set; }

        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        public string NoResultsLabel
        {
            get => noResultsLabel;
            set => SetProperty(ref noResultsLabel, value);
        }

        public bool NoResultsVisible
        {
            get => noResultsVisible;
            set => SetProperty(ref noResultsVisible, value);
        }

		async Task OnSearch(string filter = null)
        {
            NoResultsVisible = false;

            // Need query and/or filter to search
            if (!string.IsNullOrWhiteSpace(SearchQuery) || !string.IsNullOrWhiteSpace(filter))
            {
                RecipeData recipeData = await _restService.GetRecipeDataAsync(GenerateRequestUri(Constants.EdamamEndpoint, filter));

                if (recipeData == null || recipeData.Hits.Length == 0)
                {
                    NoResultsLabel = $"Sorry - we couldn't find any recipes for {SearchQuery} :(";
                    NoResultsVisible = true;
                    RecipeTypeButtonsVisible = false;
                }
                else
                {
                    NoResultsVisible = false;

                    string urlEncodedFilter = System.Net.WebUtility.UrlEncode(filter);
                    await Shell.Current.GoToAsync($"{nameof(RecipeSearchPage)}?SearchQuery={SearchQuery}&SearchFilter={urlEncodedFilter}");

                    SearchQuery = string.Empty;
                    RecipeTypeButtonsVisible = true;
                }
            }
        }

        string GenerateRequestUri(string endpoint, string filter)
        {
            string requestUri = endpoint;

            if (string.IsNullOrEmpty(SearchQuery) && !string.IsNullOrEmpty(filter))
            {
                requestUri += $"?q={filter.Substring(filter.IndexOf("=") + 1)}";
            }
            else
            {
                requestUri += $"?q={SearchQuery}";
            }

            requestUri += $"&app_id={Constants.EdamamAppId}";
            requestUri += $"&app_key={Constants.EdamamAppKey}";

            if (!string.IsNullOrEmpty(filter))
            {
                requestUri += $"&{filter}";
            }

            return requestUri;
        }
    }
}
