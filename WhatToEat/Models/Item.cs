using System.Collections.Generic;
using Xamarin.Forms;

namespace Recipes.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string RecipeName { get; set; }
        public string ImageUrl { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string RecipeBody { get; set; }
        public FormattedString RecipeUrl { get; set; }
    }
}