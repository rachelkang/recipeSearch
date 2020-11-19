using System;
using System.Globalization;
using System.Threading.Tasks;
using Recipes.Views;
using Xamarin.Forms;

namespace Recipes.ViewModels
{
    [QueryProperty(nameof(SearchQuery), nameof(SearchQuery))]
    [QueryProperty(nameof(SearchFilter), nameof(SearchFilter))]
    public class RecipeSearchViewModel : BaseViewModel
    {
        RestService _restService;

        RecipeData _recipeData;
        private string searchQuery;
        private string noResultsLabel;
        private bool noResultsLabelVisible;
        bool _searchResultsVisible;
        public Command<Hit> ItemTapped { get; }
        public Command SearchCommand { get; }

        public RecipeSearchViewModel()
        {
            Title = "Search all recipes";
            _restService = new RestService();
            NoResultsLabelVisible = false;
            SearchResultsVisible = true;

            ItemTapped = new Command<Hit>(OnItemSelected);
            SearchCommand = new Command(async () => await OnSearch());
        }

        public RecipeData RecipeData
        {
            get => _recipeData;
            set => SetProperty(ref _recipeData, value);
        }

        public string SearchQuery
        {
            get => searchQuery;
            set => SetProperty(ref searchQuery, value);
        }

        public string SearchFilter { get; set; }

        public string NoResultsLabel
        {
            get => noResultsLabel;
            set => SetProperty(ref noResultsLabel, value);
        }

        public bool NoResultsLabelVisible
        {
            get => noResultsLabelVisible;
            set => SetProperty(ref noResultsLabelVisible, value);
        }

        public bool SearchResultsVisible
        {
            get => _searchResultsVisible;
            set => SetProperty(ref _searchResultsVisible, value);
        }

        async Task OnSearch()
        {
            NoResultsLabelVisible = false;

            if (!string.IsNullOrWhiteSpace(SearchQuery) || !string.IsNullOrWhiteSpace(SearchFilter))
            {
                RecipeData recipeData = await _restService.GetRecipeDataAsync(GenerateRequestUri(Constants.EdamamEndpoint));

                if (recipeData == null || recipeData.Hits.Length == 0)
                {
                    NoResultsLabel = $"Sorry - we couldn't find any recipes for {SearchQuery} :(";
                    NoResultsLabelVisible = true;
                    SearchResultsVisible = false;
                }
                else
                {
                    NoResultsLabelVisible = false;
                    SearchResultsVisible = true;

                    for (int i = 0; i < recipeData.Hits.Length; i++)
                    {
                        recipeData.Hits[i].Id = i;
                    }

                    RecipeData = recipeData;
                    AppShell.Data = RecipeData;

                    //OnPropertyChanged(nameof(RecipeData)); // tells Xaml view to update
                }
            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string searchFilterName = SearchFilter.Substring(SearchFilter.IndexOf("=") + 1);

            if (string.IsNullOrEmpty(SearchQuery))
            {
                SearchQuery = searchFilterName;
            }

            string requestUri = endpoint;
            requestUri += $"?q={SearchQuery}";
            requestUri += $"&app_id={Constants.EdamamAppId}";
            requestUri += $"&app_key={Constants.EdamamAppKey}";

            if (!string.IsNullOrEmpty(SearchFilter))
            {
                Title = $"Search {searchFilterName} recipes";
                requestUri += $"&{SearchFilter}";
            }

            return requestUri;
        }

        async void OnItemSelected(Hit hit)
        {
            if (hit == null)
                return;

            // This will push the HitDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(HitDetailPage)}?HitId={hit.Id}");
        }
    }
}
