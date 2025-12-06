using SQLite;
using System.Linq;
using SchijfVanVijf.Models;

namespace SchijfVanVijf.Data;

public class Database
{
    //create database object used for interacting with the SQLite DB
    private SQLiteAsyncConnection database; // asyncConnetie met de database
    private DatabaseSeeder seeder;// stopt bij het opstarten de data in de database

    public async Task Init() // checkt of de databse al bestaar
    {

        if (File.Exists(Constants.DatabasePath)) // verwijderd het locale bestand als het bestaat zodat de nieuwe tables kunnen worden ingevoerd TEMP
        {
            File.Delete(Constants.DatabasePath);
        }


        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags); // maak nieuwe connectie met SQLiteAsyncConnetie 
        await database.ExecuteAsync("PRAGMA foreign_keys = ON;"); // zet het gebruik van foreign keys aan en moet direct gebeuren
        await CreateTablesAsync(); // zorgt dat talleben bestaan
        await SeedDatabaseAsync(); // vult de tabellen met data
    }

    private async Task CreateTablesAsync() // maakt tabellen aan voor database
    {

        await database.ExecuteAsync(@"CREATE TABLE ""Ingredient"" (
	                                ""Ingredient_Id""	INTEGER,
	                                ""Name""	TEXT,
                                    ""Category"" TEXT,
                                    ""Allergies"" TEXT,
	                                PRIMARY KEY(""Ingredient_Id"")
                                    );"
                                    );
        await database.ExecuteAsync(@"CREATE TABLE ""Recipe"" (
	                                ""Recipe_Id""	INTEGER,
	                                ""Title""	TEXT,
	                                ""Discription""	TEXT,
	                                ""Instruction""	TEXT,
	                                ""Preparation_Time""	INTEGER,
	                                ""Cooking_Time""	INTEGER,
	                                ""Servings""	INTEGER,
	                                PRIMARY KEY(""Recipe_Id"")
                                    );"
                                    );
        await database.ExecuteAsync(@"CREATE TABLE ""RecipeIngredient"" (
                                    ""Recipe_Ingredient_Id""	INTEGER,
	                                ""Recipe_Id""	INTEGER,
	                                ""Ingredient_Id""	INTEGER,
	                                ""Amount""	TEXT,
	                                PRIMARY KEY(""Recipe_Id"",""Ingredient_Id""),
	                                FOREIGN KEY(""Ingredient_Id"") REFERENCES ""Ingredient""(""Ingredient_Id""),
	                                FOREIGN KEY(""Recipe_Id"") REFERENCES ""Recipe""(""Recipe_Id"")
                                    );"
                                    );
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
        return await database.Table<Ingredient>() // vraagt ingrediënten tabel op
            .Where(i => i.Ingredient_Id == Id) // filtert op id
            .FirstOrDefaultAsync(); // geeft het eerstvolgende ingrediënt terug, of null
    }
    
    //TODO:
    ///=========================
    /// Recipe CRUD 
    ///=========================
    public async Task<Recipe> GetRecipeAsync(int Id)
    {
        return await database.Table<Recipe>() // vraagt ingrediënten tabel op
            .Where(i => i.Recipe_Id == Id) // filtert op id
            .FirstOrDefaultAsync(); // geeft het eerstvolgende ingrediënt terug, of null
    }

    public async Task<List<Recipe>> GetRecipeListAsync(List<int> Ids)
    {
        if (Ids == null || Ids.Count == 0) // Als lijst met ID's leeg is, return lege lijst
        {
            return new List<Recipe>();  
        }

        return await database.Table<Recipe>() // alle recepten ophalen waar het ID in de lijst zit
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

        var recipeIds = (await database.Table<RecipeIngredient>() // zoekt in RecipeIngredient alle rijen waar IngredientId in de lijst zit
            .Where(ri => ingredientIds.Contains(ri.Ingredient_Id)) // zorgt ervoor dat er alleen rijen opver blijven één van de ingredientID lijst
            .ToListAsync())
            .Select(ri => ri.Recipe_Id) // selecteer uit de gevonden koppelling alleen de RecipeId
            .Distinct() // Verwijderd de dubbele RecipeId
            .ToList();

        return await GetRecipeListAsync(recipeIds); // Haal de volledige Recipe-objecten op die horen bij het gevonden RecipeId
    }

}
