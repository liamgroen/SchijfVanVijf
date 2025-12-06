using SchijfVanVijf.Models;
using SQLite;
using System.Diagnostics;
using System.Text;

namespace SchijfVanVijf.Data;

public class DatabaseSeeder
{
    private readonly SQLiteAsyncConnection database;

    public DatabaseSeeder(SQLiteAsyncConnection db)
    {
        database = db;
    }

    public async Task SeedAsync()
    {
        try
        {
            if (await database.Table<Recipe>().CountAsync() > 0) // dit zorgt ervoor dat er niet dubbel geseed wordt, nu gebeurd dit altijd want bestand wordtt verwijderd
            {
                Debug.WriteLine("Seeden is al gebeurd en wordt overgeslagen");
                return;
            }

            Debug.WriteLine("Begint met seeden SQLite Queries");

            await ExecuteSqlAsync("SQLite_Queries_database.txt"); //bestand waar de SQLite Queries vandaan gehaald worden

            var ingCount = await database.Table<Ingredient>().CountAsync();
            var recCount = await database.Table<Recipe>().CountAsync();
            var linkCount = await database.Table<RecipeIngredient>().CountAsync();

            Debug.WriteLine($"Seeden is Klaar. Ingredients={ingCount}, Recipes={recCount}, RecipeIngredients={linkCount}");
        }
        catch (Exception ex) // geeft een foutmelding als er iets fout gaat met seeden
        {
            Debug.WriteLine("Fout met seeden" + ex);
        }
    }

    private async Task ExecuteSqlAsync(string fileName)
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(fileName); // het sql bestand wordt geopend 
        using var reader = new StreamReader(stream);

        var sb = new StringBuilder(); // we slaan de juiste sql regels op om later uit te voeren, dus zonder bijvoorbeeld spaties en --
        string? line;
        while ((line = await reader.ReadLineAsync()) != null) // leest de file regel voor regel zodat we het aan elkaar kunnen plakken
        {
            var trimmed = line.TrimStart(); // haalt de spaties in het begin weg
            if (trimmed.StartsWith("--")) // zorgt ervoor dat de regels met -- worden overgeslagen want die moeten we niet gebruiken en dus slaan we over
                continue;

            sb.AppendLine(line);
        }

        var sql = sb.ToString(); // complete SQLite die nog verder opgesplitst moet worden

        var commands = sql
            .Split(';') // alle delen opslitsen door ; 
            .Select(c => c.Trim()) // haalt de lege spaties weg
            .Where(c => !string.IsNullOrWhiteSpace(c)); // zorgt ervoor dat de lege plekken weg gaan

        foreach (var cmd in commands) // voert elke sql query los uit
        {
            await database.ExecuteAsync(cmd); // wordt één voor één uitgevoerd
        }
    }

}
