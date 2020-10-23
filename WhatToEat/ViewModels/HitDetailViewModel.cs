using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Recipes.Models;
using Recipes.Views;
using System.Windows.Input;
using Xamarin.Essentials;
using System.Linq;

namespace Recipes.ViewModels
{
    [QueryProperty(nameof(HitId), nameof(HitId))]
    public class HitDetailViewModel : BaseViewModel
    {

        public Hit Hit { get; set; }

        private string hitId;
        private string recipeName;
        private string imageUrl;
        private string ingredients;
        private FormattedString recipeBody;

        public ICommand TapCommand { get; }
        public Command AddItemCommand { get; }

        public HitDetailViewModel()
        {
            // Launcher.OpenAsync is provided by Xamarin.Essentials.
            TapCommand = new Command<string>(async (url) => await Launcher.OpenAsync(url));
            AddItemCommand = new Command(OnAddItem);
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

        private void OnAddItem()
        {
            Item newItem = new Item()
            {
                Id = HitId,
                RecipeName = RecipeName,
                ImageUrl = ImageUrl,
                Ingredients = Ingredients,
                RecipeBody = RecipeBody
            };

            DataStore.AddItemAsync(newItem);

            // add Toast that says "Added to your recipes!"
        }

        public string HitId
        {
            get
            {
                return hitId;
            }
            set
            {
                hitId = value;
                LoadHitId(value);
            }
        }

        public void LoadHitId(string hitId)
        {
            try
            {
                Hit = AppShell.Data.Hits.FirstOrDefault(h => h.Id == int.Parse(hitId));
                OnPropertyChanged(nameof(Hit));
                LoadHitDetails(Hit);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Hit");
            }
        }

        public void LoadHitDetails(Hit hit)
        {
            Title = Hit.Recipe.RecipeName;

            RecipeName = Hit.Recipe.RecipeName;
            ImageUrl = Hit.Recipe.ImageUrl;
            Ingredients = String.Join(Environment.NewLine, Hit.Recipe.Ingredients);

            var recipeBodyFormattedString = new FormattedString();
            recipeBodyFormattedString.Spans.Add(new Span { Text = "Click " });

            var recipeUrlFormattedString = new Span { Text = "here", TextColor = Color.Blue, TextDecorations = TextDecorations.Underline };
            recipeUrlFormattedString.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = TapCommand,
                CommandParameter = Hit.Recipe.RecipeUrl
            });
            recipeBodyFormattedString.Spans.Add(recipeUrlFormattedString);

            recipeBodyFormattedString.Spans.Add(new Span { Text = " to view full recipe." });
            RecipeBody = recipeBodyFormattedString;
        }
    }
}