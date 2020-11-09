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

        private bool recipeTypeButtonsVisible;
        private string noResultsLabel;
        private bool noResultsVisible;

        public Command SearchCommand { get; }
        public Command<Hit> BalancedMealsTapped { get; }

        public StartingPageViewModel()
        {
            Title = "Recipes";
            _restService = new RestService();
            NoResultsVisible = false;
            RecipeTypeButtonsVisible = true;

            SearchCommand = new Command(async () => await OnSearch());
            BalancedMealsTapped = new Command<Hit>(OnBalancedMealsSelected);
        }

        public bool RecipeTypeButtonsVisible
        {
            get => recipeTypeButtonsVisible;
            set => SetProperty(ref recipeTypeButtonsVisible, value);
        }

        public RecipeData RecipeData { get; set; }

        public string SearchQuery { get; set; }

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
                    RecipeTypeButtonsVisible = false;
                }
                else
                {
                    NoResultsVisible = false;
                    await Shell.Current.GoToAsync($"{nameof(RecipeSearchPage)}?SearchQuery={SearchQuery}");
                }
            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={SearchQuery}";
            requestUri += $"&app_id={Constants.EdamamAppId}";
            requestUri += $"&app_key={Constants.EdamamAppKey}";
            //requestUri += $"&from=0&to=1";
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
