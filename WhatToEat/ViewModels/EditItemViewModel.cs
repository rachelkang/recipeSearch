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
        // need to get selected item's stuff
        private string recipeName;
        private string ingredients;
        private string recipeBody;
        private string id;

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

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                RecipeName = item.RecipeName;
                Ingredients = item.Ingredients;
                RecipeBody = item.RecipeBody;
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
                Id = id, // need to pass in ID of selected item somehow
                RecipeName = RecipeName,
                Ingredients = Ingredients,
                RecipeBody = RecipeBody
            };

            await DataStore.UpdateItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
