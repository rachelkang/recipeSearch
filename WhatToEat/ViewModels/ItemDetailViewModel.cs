using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Recipes.Models;
using Recipes.Views;
using Xamarin.Forms;

namespace Recipes.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))] // name of property
    public class ItemDetailViewModel : BaseViewModel
    {
        public Command EditItemCommand { get; }

        private string itemId;
        private string recipeName;
        private string imageUrl;
        private string ingredients;
        private string recipeBody;
        private FormattedString recipeUrl;
        //private bool isMyRecipe;

        private bool recipeNameVisible;
        private bool imageUrlVisible;
        private bool ingredientsVisible;
        private bool recipeBodyVisible;
        private bool recipeUrlVisible;

        public string Id { get; set; }

        public ItemDetailViewModel()
        {
            Title = $"{recipeName}";
            EditItemCommand = new Command(OnEditItem);

            RecipeNameVisible = true;
            ImageUrlVisible = true;
            IngredientsVisible = true;
            RecipeBodyVisible = true;
            RecipeUrlVisible = true;
        }

        //private bool canEditRecipe(object arg)
        //{
        //    return IsMyRecipe;
        //}

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                if (value == null)
                    return;

                itemId = value;
                LoadItemId(value);
            }
        }

        public string RecipeName
        {
            get => recipeName;
            set => SetProperty(ref recipeName, value);
        }

        public string ImageUrl
        {
            get => imageUrl;
            set => SetProperty(ref imageUrl, value);
        }

        public string Ingredients
        {
            get => ingredients;
            set => SetProperty(ref ingredients, value);
        }

        public string RecipeBody
        {
            get => recipeBody;
            set => SetProperty(ref recipeBody, value);
        }

        public FormattedString RecipeUrl
        {
            get => recipeUrl;
            set => SetProperty(ref recipeUrl, value);
        }

        //public bool IsMyRecipe
        //{
        //    get => isMyRecipe;
        //    set => SetProperty(ref isMyRecipe, value);
        //}

        public bool RecipeNameVisible
        {
            get => recipeNameVisible;
            set => SetProperty(ref recipeNameVisible, value);
        }

        public bool ImageUrlVisible
        {
            get => imageUrlVisible;
            set => SetProperty(ref imageUrlVisible, value);
        }

        public bool IngredientsVisible
        {
            get => ingredientsVisible;
            set => SetProperty(ref ingredientsVisible, value);
        }

        public bool RecipeBodyVisible
        {
            get => recipeBodyVisible;
            set => SetProperty(ref recipeBodyVisible, value);
        }

        public bool RecipeUrlVisible
        {
            get => recipeUrlVisible;
            set => SetProperty(ref recipeUrlVisible, value);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                RecipeName = item.RecipeName;
                ImageUrl = item.ImageUrl;
                Ingredients = item.Ingredients;
                RecipeBody = item.RecipeBody;
                RecipeUrl = item.RecipeUrl;
                //IsMyRecipe = item.IsMyRecipe;

                var emptyFormattedString = new FormattedString();
                emptyFormattedString.Spans.Add(new Span { Text = "" });

                RecipeNameVisible = !String.IsNullOrEmpty(RecipeName);
                ImageUrlVisible = !String.IsNullOrEmpty(ImageUrl);
                IngredientsVisible = !String.IsNullOrEmpty(Ingredients);
                RecipeBodyVisible = !String.IsNullOrEmpty(RecipeBody);
                RecipeUrlVisible = !(RecipeUrl == null || FormattedString.Equals(RecipeUrl, emptyFormattedString));
                
            }
            catch (Exception) 
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnEditItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(EditItemPage)}?{nameof(EditItemViewModel.Id)}={itemId}");
        }

        public void OnAppearing()
        {
            IsBusy = true;
            LoadItemId(itemId);
        }
    }
}
