using System;
using System.Globalization;
using System.Threading.Tasks;
using Recipes.Views;
using Xamarin.Forms;

namespace Recipes.ViewModels
{
    public class RecipeSearchViewModel : BaseViewModel
    {
        RestService _restService;

        public Command<Hit> ItemTapped { get; }
        public Command SearchCommand { get; }

        public RecipeSearchViewModel()
        {
            Title = "Search Recipes";
            _restService = new RestService();
            ButtonText = "Search";
            SearchQuery = "Macaroni";
            //NoResultsLabel = $"Sorry - we couldn't find any recipes :(";

            ItemTapped = new Command<Hit>(OnItemSelected);
            SearchCommand = new Command(async () => await OnSearch());

        }

        public RecipeData RecipeData { get; set; }

        public string ButtonText { get; set; }

        public string SearchQuery { get; set; }

        public string NoResultsLabel { get; set; }

        async Task OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                RecipeData recipeData = await _restService.GetRecipeDataAsync(GenerateRequestUri(Constants.EdamamEndpoint));

                if (recipeData.Hits.Length == 0 || recipeData == null)
                {
                    //NoResultsLabel = $"Sorry - we couldn't find any recipes for {SearchQuery} :(";
                    //noResultsLabel.IsVisible = true;
                }
                else
                {
                    //noResultsLabel.IsVisible = false;
                }

                for (int i = 0; i < recipeData.Hits.Length; i++)
                {
                    recipeData.Hits[i].Id = i;
                }

                RecipeData = recipeData;
                AppShell.Data = RecipeData;
                OnPropertyChanged(nameof(RecipeData)); // tells Xaml view to update
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

        async void OnItemSelected(Hit hit)
        {
            if (hit == null)
                return;

            // This will push the HitDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(HitDetailPage)}?HitId={hit.Id}");
        }

        //public class InverseBoolConverter : IValueConverter
        //{
        //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        return !((bool)value);
        //    }

        //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        return value;
        //    }
        //}
    }
}
