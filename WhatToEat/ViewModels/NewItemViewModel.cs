using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Recipes.Models;
using Xamarin.Forms;

namespace Recipes.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string recipeName;
        private string ingredients;
        private string recipeBody;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(recipeName)
                && !String.IsNullOrWhiteSpace(ingredients)
                && !String.IsNullOrWhiteSpace(recipeBody);
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                RecipeName = RecipeName,
                Ingredients = Ingredients,
                RecipeBody = RecipeBody
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
