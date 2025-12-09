using System.Collections.ObjectModel;
using SchijfVanVijf.Models;
using System.Threading.Tasks;
using SchijfVanVijf.Data;

namespace SchijfVanVijf.Views;
public partial class SearchPage : ContentPage
{

    //this is the list of  ingredients
    public ObservableCollection<IngredientItem> IngredientList { get; set; } = new ();
    private readonly Database _database;

    public SearchPage()
    {
        InitializeComponent();

        BindingContext = this;
        _database = IPlatformApplication.Current.Services.GetRequiredService<Database>();
        IngredientList.Add(new IngredientItem { Name = "" });
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        // set the last item in IngredientList to the users Entry 
        var lastItem = IngredientList.LastOrDefault();

        //checking if the last item isnt empty
        if (lastItem != null && string.IsNullOrWhiteSpace(lastItem.Name))
        {
            DisplayAlert("Info", "Please fill in the last ingredient before adding a new one.", "OK");
            return;
        }

        // add a new entry to the observablecollection, which becomes the new last item for the next OnAddClicked
        IngredientList.Add(new IngredientItem { Name = "" });
    }

    private async void OnSearchClicked(object sender, EventArgs e)
    {
        //TODO: Should search database for matching ingredients and hand back a list of ingredients to be shown on the recipe main page
        //await DisplayAlert("Search", $"Searching with {IngredientList.Count} ingredients...", "OK");

        List<string> ingredientsInHouse = IngredientList.Select(i => i.Name).ToList();
        var ingredientIds = await _database.GetIngredientIdsForNames(ingredientsInHouse);

        new NavigationPage(new MainPage());
        await Navigation.PushAsync(page: new RecipeMainPage(ingredientIds));
    }


    //use this to be more robust instead of searching the DB with user entered names



    private async void GoToFilterMainPage_Clicked(object sender, EventArgs e)
    {
        new NavigationPage(new MainPage());
        await Navigation.PushAsync(page: new FilterMainPage());
    }
}

public class IngredientItem
{
    public string Name { get; set; }
}