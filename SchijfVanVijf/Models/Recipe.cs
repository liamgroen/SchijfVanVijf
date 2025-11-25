using SQLite;

namespace SchijfVanVijf.Models;

public class Recipe
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(250)]
    public string Name { get; set; }

    public string Instructions { get; set; }

    public int PreparationTimeMinutes { get; set;}

    public int Servings { get; set; }
}