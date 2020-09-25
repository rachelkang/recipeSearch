using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Recipes
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://api.edamam.com/");
        }

        public async Task<RecipeData> GetRecipeDataAsync(string uri)
        {

            //List<RecipeData> recipes = new List<RecipeData>();
            RecipeData recipeData = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    recipeData = JsonConvert.DeserializeObject<RecipeData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            Debug.WriteLine(recipeData);
            return recipeData;
        }
    }
}