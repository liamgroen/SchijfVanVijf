using Microsoft.Maui.Storage;

namespace SchijfVanVijf.Data;

public static class Constants
{
    public const string DatabaseFilename = "SchijfVanVijf.db3"; // implementatie voor de naam voor de SQLite-databasebestand

    public const SQLite.SQLiteOpenFlags Flags = // flags die nodig zijn voor onze databaseintegratie
        SQLite.SQLiteOpenFlags.ReadWrite | // opent de database in de nodige read/write modus
        SQLite.SQLiteOpenFlags.Create | // maakt een database aan als deze nog niet bestaat
        SQLite.SQLiteOpenFlags.SharedCache | // geeft toegang tot mulit-treaded toegang tot de database
        SQLite.SQLiteOpenFlags.ProtectionComplete; // Het bestand is gecodeerd en ontoegankelijk zolang het apparaat is vergrendeld, dit zorgt voor extra veiligheid

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename); // maakt de map, verzorgd de bestandslocatie waar de database wordt opgeslagen

}