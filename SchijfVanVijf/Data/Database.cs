using SQLite;
using SchijfVanVijf.Models;

namespace SchijfVanVijf.Data;

public class Database
{
    SQLiteAsyncConnection database;

    async Task Init()
    {
        if (database is not null)
        {
            return;
        }

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await CreateTablesAsync();
        await SeedDatabaseAsync();
        
    }

    private async Task CreateTablesAsync()
    {
        await database.CreateTableAsync<Ingredient>();
        await database.CreateTableAsync<Recipe>();
        await database.CreateTableAsync<RecipeIngredient>();
    }

    private async SeedDatabaseAsync()
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
    
    //TODO:
    ///=========================
    /// RecipeIngredients CRUD 
    ///=========================
    
}
