using SQLite;

namespace SchijfVanVijf.Models;

public class Ingredient
{
    [PrimaryKey, AutoIncrement]
    public int Ingredient_Id { get; set; }

    [MaxLength(250), Unique]
    public string Name { get; set; }

    public string Category { get; set; }

    public string Allergies { get; set; }
}