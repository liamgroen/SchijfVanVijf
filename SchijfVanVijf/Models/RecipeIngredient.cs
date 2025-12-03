using SQLite;

namespace SchijfVanVijf.Models;

public class RecipeIngredient
{
    [Indexed]
    public int Recipe_Id { get; set; }

    [Indexed]
    public int Ingredient_Id { get; set; }

    public int Amount { get; set; }
}
