using System.Collections.ObjectModel;
using SchijfVanVijf.Models;
using SchijfVanVijf.Data;
namespace SchijfVanVijf.Views;

public partial class RecipeSelectedPage : ContentPage
{
    public Recipe Recipe { get; set; }
	public ObservableCollection<Ingredient> Ingredients { get; set; } = new();
	private readonly Database _database;

    public RecipeSelectedPage(Recipe clickedRecipe)
	{
		InitializeComponent();
		Recipe = clickedRecipe;
		BindingContext = this;

		_database = IPlatformApplication.Current.Services.GetRequiredService<Database>();
		LoadIngredientsAsync();
	}


	public async void LoadIngredientsAsync()
    {
        var ingredients = await _database.GetIngredientListForRecipe(Recipe.Recipe_Id);
		foreach (Ingredient ingredient in ingredients)
        {
            Ingredients.Add(ingredient);
        }
    }


	public async void OnScrollViewScrolled (Object sender, ScrolledEventArgs e)
	{   
    }


}