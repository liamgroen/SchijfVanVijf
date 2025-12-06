using SQLite;

namespace SchijfVanVijf.Models;

public class Recipe
{
    [PrimaryKey, AutoIncrement]
    public int Recipe_Id { get; set; }

    [MaxLength(250)]
    public string Title { get; set; }
    public string Discription { get; set; }
    public string Instruction { get; set;}
    public int Preparation_Time { get; set; }
    public int Cooking_Time { get; set; }
    public int Servings { get; set; }
}