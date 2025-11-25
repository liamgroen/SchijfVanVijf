using SchijfVanVijf.Models;
using SQLite;

namespace SchijfVanVijf.Data;

public class DatabaseSeeder
{

    private readonly SQLiteAsyncConnection database;

    public DatabaseSeeder(SQLiteAsyncConnection _database)
    {
        database = _database;
    }

    public async Task SeedAsync()
    {
        await SeedIngredientAsync();
        await SeedRecipeAsync();
        await SeedRecipeIngredientAsync();
    }

    private async Task SeedIngredientAsync()
    {
        if (await database.Table<Ingredient>().CountAsync() > 0)
        {
            return;
        }

        var ingredients = new List<Ingredient>
        {
            new Ingredient { Name = "Onion"},
            new Ingredient { Name = "Broccoli"}
        };

        await database.InsertAllAsync(ingredients);
    }

    private async Task SeedRecipeAsync()
    {
        if (await database.Table<Recipe>().CountAsync() > 0)
        {
            return;
        }

        var recipes = new List<Recipe>
        {
            new Recipe { Name = "Apple Pie", Instructions = "1. Make the pie. 2. Enjoy", PreparationTimeMinutes = 30, Servings = 4}
        };

        await database.InsertAllAsync(recipes);
    }

    private async Task SeedRecipeIngredientAsync()
    {
        if (await database.Table<RecipeIngredient>().CountAsync() > 0)
        {
            return;
        }

        var recipeIngredients = new List<RecipeIngredient>
        {
            new RecipeIngredient { RecipeId = 1, IngredientId = 1, Quantity = "500g", Notes = "Sliced"}
        };

        await database.InsertAllAsync(recipeIngredients);
    }
}