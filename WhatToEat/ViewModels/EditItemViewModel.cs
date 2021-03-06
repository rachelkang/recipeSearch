﻿using System;
using System.Diagnostics;
using Xamarin.Forms;
using Recipes.Models;
using System.Collections.Generic;

namespace Recipes.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))] // query string - using property names directly and avoiding magic strings
    public class EditItemViewModel : BaseViewModel
    {
        string _id;
        string _recipeName;
        string _ingredients;
        string _imageUrl;
        string _recipeBody;
        FormattedString _recipeUrl;

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
            return !String.IsNullOrWhiteSpace(_recipeName);
        }

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                LoadItemId(value);
            }
        }

        public string RecipeName
        {
            get => _recipeName;
            set => SetProperty(ref _recipeName, value);
        }

        public string Ingredients
        {
            get => _ingredients;
            set => SetProperty(ref _ingredients, value);
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        public string RecipeBody
        {
            get => _recipeBody;
            set => SetProperty(ref _recipeBody, value);
        }

        public FormattedString RecipeUrl
        {
            get => _recipeUrl;
            set => SetProperty(ref _recipeUrl, value);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                RecipeName = item.RecipeName;

                string ingredients = "";
                foreach (Ingredient ingredient in item.Ingredients)
                    ingredients += ingredient.IngredientItem + Environment.NewLine;
                Ingredients = ingredients;

                ImageUrl = item.ImageUrl;
                RecipeBody = item.RecipeBody;
                RecipeUrl = item.RecipeUrl;
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
            await DataStore.DeleteItemAsync(_id); // need to pass in ID of selected item somehow

            //Shell.Current.Navigation.RemovePage($"../../{id}");

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("../..");
        }

        private async void OnUpdate()
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            string[] ingredientStringList = Ingredients.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string ingredientString in ingredientStringList)
                ingredientList.Add(new Ingredient { IngredientItem = ingredientString });

            Item newItem = new Item()
            {
                Id = _id,
                RecipeName = RecipeName,
                Ingredients = ingredientList,
                ImageUrl = ImageUrl,
                RecipeBody = RecipeBody,
                RecipeUrl = RecipeUrl
            };

            await DataStore.UpdateItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
