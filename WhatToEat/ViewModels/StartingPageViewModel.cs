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

        string _searchQuery; // private by default, _name
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
            BalancedMealsTapped = new Command<Hit>(OnBalancedMealsSelected);
        }

        public bool RecipeTypeButtonsVisible
        {
            get => recipeTypeButtonsVisible;
            set => SetProperty(ref recipeTypeButtonsVisible, value);
        }

        public RecipeData RecipeData { get; set; }

        public string SearchQuery
        {
            get
            {
                return _searchQuery;
            }
            set
            {
                SetProperty(ref _searchQuery, value);
            }
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

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                RecipeData recipeData = await _restService.GetRecipeDataAsync(GenerateRequestUri(Constants.EdamamEndpoint));

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

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
			requestUri += $"?q={SearchQuery}";
			requestUri += $"&app_id={Constants.EdamamAppId}";
            requestUri += $"&app_key={Constants.EdamamAppKey}";

            return requestUri;
        }

        async void OnBalancedMealsSelected(Hit hit)
        {
            //if (hit == null)
            //    return;

            await Shell.Current.GoToAsync($"{nameof(RecipeSearchPage)}?DietSearchFilter=balanced");
        }
    }
}
