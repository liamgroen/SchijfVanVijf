using SQLite;

namespace SchijfVanVijf.Models;

public class Ingredient
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(250), Unique]
    public string Name { get; set; }
}