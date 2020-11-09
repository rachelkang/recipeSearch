using System;
using System.Globalization;
using System.Threading.Tasks;
using Recipes.Views;
using Xamarin.Forms;

namespace Recipes.ViewModels
{
    [QueryProperty(nameof(SearchQuery), nameof(SearchQuery))]
    [QueryProperty(nameof(DietSearchFilter), nameof(DietSearchFilter))]
    [QueryProperty(nameof(HealthSearchFilter), nameof(HealthSearchFilter))]
    public class RecipeSearchViewModel : BaseViewModel
    {
        RestService _restService;

        private string searchQuery;
        //private string dietSearchFilter;
        //private string healthSearchFilter;
        private string noResultsLabel;
        private bool noResultsVisible;
        public Command<Hit> ItemTapped { get; }
        public Command SearchCommand { get; }

        public RecipeSearchViewModel()
        {
            Title = "Search Results";
            _restService = new RestService();
            NoResultsVisible = false;

            ItemTapped = new Command<Hit>(OnItemSelected);
            SearchCommand = new Command(async () => await OnSearch());
        }

        public RecipeData RecipeData { get; set; }

        public string SearchQuery
        {
            get
            {
                return searchQuery;
            }
            set
            {
                searchQuery = value;
                _ = OnSearch();
            }
        }

        public string DietSearchFilter { get; set; }

        public string HealthSearchFilter { get; set; }

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

        async Task OnSearch()
        {
            NoResultsVisible = false;

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                RecipeData recipeData = await _restService.GetRecipeDataAsync(GenerateRequestUri(Constants.EdamamEndpoint));

                if (recipeData == null || recipeData.Hits.Length == 0)
                {
                    NoResultsLabel = $"Sorry - we couldn't find any recipes for {SearchQuery} :(";
                    NoResultsVisible = true;
                }
                else
                {
                    NoResultsVisible = false;

                    for (int i = 0; i < recipeData.Hits.Length; i++)
                    {
                        recipeData.Hits[i].Id = i;
                    }

                    RecipeData = recipeData;
                    AppShell.Data = RecipeData;
                    OnPropertyChanged(nameof(RecipeData)); // tells Xaml view to update
                }
            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={SearchQuery}";
            requestUri += $"&app_id={Constants.EdamamAppId}";
            requestUri += $"&app_key={Constants.EdamamAppKey}";
            //requestUri += $"&diet={DietSearchFilter}";
            //requestUri += $"&health={HealthSearchFilter}";
            //requestUri += $"&from=0&to=1";
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
