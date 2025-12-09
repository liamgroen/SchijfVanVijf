using SQLite;
using System.Linq;
using SchijfVanVijf.Models;

namespace SchijfVanVijf.Data;

public class Database
{
    //create database object used for interacting with the SQLite DB
    private SQLiteAsyncConnection _db; // asyncConnetie met de database
    private DatabaseSeeder seeder;// stopt bij het opstarten de data in de database
    private bool _initialized;
    private readonly SemaphoreSlim _initLock = new(1, 1);

    public Database()
    {
        _db = new SQLiteAsyncConnection(
            Constants.DatabasePath,
            Constants.Flags
        );
    }

    /// <summary>
    /// Safe to call multiple times – only runs once.
    /// </summary>
    public async Task Init()
    {
        if (_initialized)
            return;

        await _initLock.WaitAsync();
        try
        {
            if (_initialized)
                return;

            await _db.ExecuteAsync("PRAGMA foreign_keys = ON;");

            await CreateTables();
            await SeedDatabase();

            _initialized = true;
        }
        finally
        {
            _initLock.Release();
        }
    }

    private async Task CreateTables()
    {
        await _db.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS Ingredient (
                Ingredient_Id INTEGER PRIMARY KEY,
                Name TEXT,
                Category TEXT,
                Allergies TEXT
            );
        """);

        await _db.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS Recipe (
                Recipe_Id INTEGER PRIMARY KEY,
                Title TEXT,
                Discription TEXT,
                Instruction TEXT,
                Preparation_Time INTEGER,
                Cooking_Time INTEGER,
                Servings INTEGER
            );
        """);

        await _db.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS RecipeIngredient (
                Recipe_Id INTEGER,
                Ingredient_Id INTEGER,
                Amount TEXT,
                PRIMARY KEY (Recipe_Id, Ingredient_Id),
                FOREIGN KEY (Recipe_Id) REFERENCES Recipe(Recipe_Id),
                FOREIGN KEY (Ingredient_Id) REFERENCES Ingredient(Ingredient_Id)
            );
        """);
    }
    
    private async Task SeedDatabase() // maakt een databaseSeeder
    {
        var seeder = new DatabaseSeeder(_db);
        await seeder.SeedAsync();
    }
    //TODO:
    ///=========================
    /// Ingredient CRUD 
    ///=========================
    public async Task<Ingredient> GetIngredientAsync(int Id)
    {
        await Init();
        return await _db.Table<Ingredient>() // vraagt ingredi�nten tabel op
            .Where(i => i.Ingredient_Id == Id) // filtert op id
            .FirstOrDefaultAsync(); // geeft het eerstvolgende ingredi�nt terug, of null
    }
    
    //TODO:
    ///=========================
    /// Recipe CRUD 
    ///=========================
    public async Task<Recipe> GetRecipeAsync(int Id)
    {
        await Init();
        return await _db.Table<Recipe>() // vraagt ingredi�nten tabel op
            .Where(i => i.Recipe_Id == Id) // filtert op id
            .FirstOrDefaultAsync(); // geeft het eerstvolgende ingredi�nt terug, of null
    }

    public async Task<List<Recipe>> GetRecipeListAsync(List<int> Ids)
    {
        if (Ids == null || Ids.Count == 0) // Als lijst met ID's leeg is, return lege lijst
        {
            return new List<Recipe>();  
        }

        await Init();
        return await _db.Table<Recipe>() // alle recepten ophalen waar het ID in de lijst zit
            .Where(i => Ids.Contains(i.Recipe_Id))
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
        if (ingredientIds == null || ingredientIds.Count == 0) // Als lijst met ingredientsID's leeg is, return lege lijst
        { 
        return new List<Recipe>();
        }   
        
        await Init();
        var recipeIds = (await _db.Table<RecipeIngredient>() // zoekt in RecipeIngredient alle rijen waar IngredientId in de lijst zit
            .Where(ri => ingredientIds.Contains(ri.Ingredient_Id)) // zorgt ervoor dat er alleen rijen opver blijven ��n van de ingredientID lijst
            .ToListAsync())
            .Select(ri => ri.Recipe_Id) // selecteer uit de gevonden koppelling alleen de RecipeId
            .Distinct() // Verwijderd de dubbele RecipeId
            .ToList();

        return await GetRecipeListAsync(recipeIds); // Haal de volledige Recipe-objecten op die horen bij het gevonden RecipeId
    }

    // Returns a list of ingredients that belong to a category
    public async Task<List<string>> GetIngredientsForCategory(string category)
    {
        if (category == null)
        {
            return new List<string>();
        }

        await Init();
        var ingredientList = (await _db.Table<Ingredient>()
            .Where(ri => ri.Category == category)
            .ToListAsync())
            .Select(ri => ri.Name)
            .ToList();

        return ingredientList;
    }
}
