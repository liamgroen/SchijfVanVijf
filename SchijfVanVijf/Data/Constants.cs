using Microsoft.Maui.Storage;

namespace SchijfVanVijf.Data;

public static class Constants
{
    public const string DatabaseFilename = "Database.db";

    public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;

    // Gebruik databasebestand uit de repo (Data/ folder)
    public static string DatabasePath =>
        Path.Combine(FileSystem.CurrentDirectory, "Data", DatabaseFilename);
}
