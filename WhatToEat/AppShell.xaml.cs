﻿using System;
using System.Collections.Generic;
using Plugin.SharedTransitions;
using Recipes.Models;
using Recipes.ViewModels;
using Recipes.Views;
using Xamarin.Forms;

namespace Recipes
{
    public partial class AppShell : SharedTransitionShell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RecipeSearchPage), typeof(RecipeSearchPage));
            Routing.RegisterRoute(nameof(HitDetailPage), typeof(HitDetailPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(EditItemPage), typeof(EditItemPage));
        }

        public static RecipeData Data { get; set; }

        public static Item[] MyRecipes { get; set; }

    }
}
