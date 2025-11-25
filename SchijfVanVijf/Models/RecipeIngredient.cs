using SQLite;

namespace SchijfVanVijf.Models;

public class RecipeIngredient
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int RecipeId { get; set; }
    
    [Indexed]
    public int IngredientId { get; set; }

    public string Quantity { get; set; }  // bvb "2 cups", "3 pieces"
    
    public string Notes { get; set; }  // bvb "chopped", "diced", etc.
}