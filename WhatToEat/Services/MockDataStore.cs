using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recipes.Models;

namespace Recipes.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), RecipeName = "Recipe 1", Ingredients = "Ingredient 1\nIngredient 2\nIngredient 3", RecipeBody = "This is a recipe body." },
                new Item { Id = Guid.NewGuid().ToString(), RecipeName = "Recipe 2", Ingredients = "Ingredient 1\nIngredient 2\nIngredient 3", RecipeBody = "This is a recipe body." },
                new Item { Id = Guid.NewGuid().ToString(), RecipeName = "Recipe 3", Ingredients = "Ingredient 1\nIngredient 2\nIngredient 3", RecipeBody = "This is a recipe body." }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}