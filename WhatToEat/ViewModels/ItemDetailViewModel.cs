using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Recipes.Models;
using Xamarin.Forms;

namespace Recipes.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string recipeName;
        private string ingredients;
        private string recipeBody;

        public string Id { get; set; }

        public string RecipeName
        {
            get => recipeName;
            set => SetProperty(ref recipeName, value);
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

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
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
                Ingredients = item.Ingredients;
                RecipeBody = item.RecipeBody;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
