using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Recipes.Models;
using Recipes.Views;
using Xamarin.Forms;

namespace Recipes.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))] // query string - using property names directly and avoiding magic strings
    public class EditItemViewModel : BaseViewModel
    {
        private string id;
        private string recipeName;
        private string ingredients;
        private string imageUrl;
        private string recipeBody;
        private FormattedString recipeUrl;
        //private bool isMyRecipe;

        public EditItemViewModel()
        {
            UpdateCommand = new Command(OnUpdate, ValidateUpdate);
            DeleteCommand = new Command(OnDelete);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => UpdateCommand.ChangeCanExecute();
            this.PropertyChanged +=
                (_, __) => DeleteCommand.ChangeCanExecute();
        }

        private bool ValidateUpdate()
        {
            return !String.IsNullOrWhiteSpace(recipeName);
        }

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                LoadItemId(value);
            }
        }

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

        public string ImageUrl
        {
            get => imageUrl;
            set => SetProperty(ref imageUrl, value);
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

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                RecipeName = item.RecipeName;
                Ingredients = item.Ingredients;
                ImageUrl = item.ImageUrl;
                RecipeBody = item.RecipeBody;
                RecipeUrl = item.RecipeUrl;
                //IsMyRecipe = item.IsMyRecipe;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public Command UpdateCommand { get; }
        public Command DeleteCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync(".."); // .. passes data (and clears out data / previous query properties); doesn't just go back and do nothing
        }

        private async void OnDelete()
        {
            await DataStore.DeleteItemAsync(id); // need to pass in ID of selected item somehow

            //Shell.Current.Navigation.RemovePage($"../../{id}");

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("../..");
        }

        private async void OnUpdate()
        {
            Item newItem = new Item()
            {
                Id = id,
                RecipeName = RecipeName,
                Ingredients = Ingredients,
                ImageUrl = ImageUrl,
                RecipeBody = RecipeBody,
                RecipeUrl = RecipeUrl
                //IsMyRecipe = IsMyRecipe
            };

            await DataStore.UpdateItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
