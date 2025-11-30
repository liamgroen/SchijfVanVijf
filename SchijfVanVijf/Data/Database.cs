using SQLite;
using System.Linq;
using SchijfVanVijf.Models;

namespace SchijfVanVijf.Data;

public class Database
{
    //create database object used for interacting with the SQLite DB
    private SQLiteAsyncConnection database;
    private DatabaseSeeder seeder;

    private async Task Init()
    {
        if (database is not null)
        {
            return;
        }

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await CreateTablesAsync();
        
        // DEBUG: laat zien welke database wordt geopend
        Console.WriteLine("DEBUG - Using database at: " + Constants.DatabasePath);

        // Seed database (voegt alleen toe wat nog ontbreekt, overschrijft niets)
        await SeedDatabaseAsync();


    }

    //Make sure that tables exist>
    private async Task CreateTablesAsync()
    {
        await database.CreateTableAsync<Ingredient>();
        await database.CreateTableAsync<Recipe>();
        await database.CreateTableAsync<RecipeIngredient>();
    }
    
    private async Task SeedDatabaseAsync()
    {
        seeder = new DatabaseSeeder(database);
        await seeder.SeedAsync();
    }
    //TODO:
    ///=========================
    /// Ingredient CRUD 
    ///=========================
    public async Task<Ingredient> GetIngredientAsync(int Id)
    {
        await Init();
        return await database.Table<Ingredient>()
            .Where(i => i.Id == Id)
            .FirstOrDefaultAsync();
    }
    
    //TODO:
    ///=========================
    /// Recipe CRUD 
    ///=========================
    public async Task<Recipe> GetRecipeAsync(int Id)
    {
        await Init();
        return await database.Table<Recipe>()
            .Where(i => i.Id == Id)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Recipe>> GetRecipeListAsync(List<int> Ids)
    {
        if (Ids == null || Ids.Count == 0)
        {
            return new List<Recipe>();  
        }

        await Init();
        return await database.Table<Recipe>()
            .Where(i => Ids.Contains(i.Id))
            .ToListAsync();
    }

    
    //TODO:
    ///=========================
    /// RecipeIngredients CRUD 
    ///=========================
    

    // ============================================
    // RECIPE SEARCH / FILTER
    // ============================================
    public async Task<List<Recipe>> GetRecipesContainingAnyAsync(List<int> ingredientIds)
    {
        await Init();

        if (ingredientIds == null || ingredientIds.Count == 0)
            return new List<Recipe>();

        var recipeIds = (await database.Table<RecipeIngredient>()
            .Where(ri => ingredientIds.Contains(ri.IngredientId))
            .ToListAsync())
            .Select(ri => ri.RecipeId)
            .Distinct()
            .ToList();

        return await GetRecipeListAsync(recipeIds);
    }

}
