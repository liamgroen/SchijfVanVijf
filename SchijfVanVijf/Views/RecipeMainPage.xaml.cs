using System.Collections.ObjectModel;
using SchijfVanVijf.Data;
using SchijfVanVijf.Models;
namespace SchijfVanVijf.Views;

public partial class RecipeMainPage : ContentPage
{
    //Creates a observableCollection for the matching recipes
    public ObservableCollection<Recipe> RecipeList { get; set; } = new();

    private readonly Database _database;
    private List<int> ingredientsInHouse;

    
    public RecipeMainPage(List<int> ingredientIds)
	{
		InitializeComponent();
        BindingContext = this;
        _database = IPlatformApplication.Current.Services.GetRequiredService<Database>();
        ingredientsInHouse = ingredientIds;

        LoadRecipesAsync();
    }


    private async void LoadRecipesAsync()
    {
        var recipes = await _database.GetRecipesContainingAnyAsync(ingredientsInHouse);
        foreach (Recipe recipe in recipes)
        {
            RecipeList.Add(recipe);
        }
    }

    //Checks the text of the button that the user has pressed and hands this to the newly created recipe selected page
    private async void OnButtonClick(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        string text = button.Text;

        await Navigation.PushAsync(page: new Views.RecipeSelectedPage(text));
    }
}
