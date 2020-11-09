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
        private string imageUrl;
        private string ingredients;
        private string recipeBody;
        private FormattedString recipeUrl;
        //private bool isMyRecipe;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(recipeName);
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            //var recipeBodyFormattedString = new FormattedString();
            //recipeBodyFormattedString.Spans.Add(new Span { Text = RecipeBody });

            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                RecipeName = RecipeName,
                ImageUrl = ImageUrl,
                Ingredients = Ingredients,
                RecipeBody = RecipeBody,
                RecipeUrl = RecipeUrl
                //IsMyRecipe = true
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
