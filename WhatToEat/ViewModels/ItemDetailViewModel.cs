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
        private FormattedString recipeBody;

        public string Id { get; set; }

        public ItemDetailViewModel()
        {
            Title = $"{recipeName}";
            EditItemCommand = new Command(OnEditItem);
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

        public FormattedString RecipeBody
        {
            get => recipeBody;
            set => SetProperty(ref recipeBody, value);
        }

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
