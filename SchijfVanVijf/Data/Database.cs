using SQLite;
using System.Linq;
using SchijfVanVijf.Models;

namespace SchijfVanVijf.Data;

public class Database
{
    //create database object used for interacting with the SQLite DB
    private SQLiteAsyncConnection database; // asyncConnetie met de database
    private DatabaseSeeder seeder;// stopt bij het opstarten de data in de database

    private async Task Init() // checkt of de databse al bestaar
    {
        if (database is not null)
        {
            return; // ga terug als de database bestaat
        }

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags); // maak nieuwe connectie met SQLiteAsyncConnetie 
        await CreateTablesAsync(); // zorgt dat talleben bestaan
        await SeedDatabaseAsync(); // vult de tabellen met data


    }

    //Make sure that tables exist>
    private async Task CreateTablesAsync() // maakt tabellen aan 
    {
        await database.CreateTableAsync<Ingredient>();
        await database.CreateTableAsync<Recipe>();
        await database.CreateTableAsync<RecipeIngredient>();
    }
    
    private async Task SeedDatabaseAsync() // maakt een databaseSeeder
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
        await Init(); // zorgt dtat de database klaarstaat
        return await database.Table<Ingredient>() // vraagt ingrediënten tabel op
            .Where(i => i.Id == Id) // filtert op id
            .FirstOrDefaultAsync(); // geeft het eerstvolgende ingrediënt terug, of null
    }
    
    //TODO:
    ///=========================
    /// Recipe CRUD 
    ///=========================
    public async Task<Recipe> GetRecipeAsync(int Id)
    {
        await Init(); // zorgt dtat de database klaarstaat
        return await database.Table<Recipe>() // vraagt ingrediënten tabel op
            .Where(i => i.Id == Id) // filtert op id
            .FirstOrDefaultAsync(); // geeft het eerstvolgende ingrediënt terug, of null
    }

    public async Task<List<Recipe>> GetRecipeListAsync(List<int> Ids)
    {
        if (Ids == null || Ids.Count == 0) // Als lijst met ID's leeg is, return lege lijst
        {
            return new List<Recipe>();  
        }

        await Init();
        return await database.Table<Recipe>() // alle recepten ophalen waar het ID in de lijst zit
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

        if (ingredientIds == null || ingredientIds.Count == 0) // Als lijst met ingredientsID's leeg is, return lege lijst
        { 
        return new List<Recipe>();
        }   

        var recipeIds = (await database.Table<RecipeIngredient>() // zoekt in RecipeIngredient alle rijen waar IngredientId in de lijst zit
            .Where(ri => ingredientIds.Contains(ri.IngredientId)) // zorgt ervoor dat er alleen rijen opver blijven één van de ingredientID lijst
            .ToListAsync())
            .Select(ri => ri.RecipeId) // selecteer uit de gevonden koppelling alleen de RecipeId
            .Distinct() // Verwijderd de dubbele RecipeId
            .ToList();

        return await GetRecipeListAsync(recipeIds); // Haal de volledige Recipe-objecten op die horen bij het gevonden RecipeId
    }

}
