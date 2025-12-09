using System.ComponentModel;
using System.Collections.ObjectModel;
using SchijfVanVijf.ViewModel;
using SchijfVanVijf.Data;

namespace SchijfVanVijf.Views;

public partial class FilterSelectedPage : ContentPage
{
    public string Category { get; set; }
    public ObservableCollection<string> IngredientList { get; } = new();
    private readonly Database _database;


    public FilterSelectedPage(string category)
	{
		InitializeComponent();
        //BindingContext = new ViewModel.FilterSelectedPageViewModel();
        Category = category;
        BindingContext = this;
        _database = IPlatformApplication.Current.Services.GetRequiredService<Database>();

        LoadIngredientsAsync();
    }

    private async void LoadIngredientsAsync()
    {
        await _database.Init();
        var ingredients = await _database.GetIngredientsForCategory(Category);
        IngredientList.Clear();
        foreach (string ingredient in ingredients)
        {
            IngredientList.Add(ingredient);
        }
    }


    private async void OkButtonClick(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }


    //Checks the bool value of the checkbox and detmines if the corresponding ingredient should be added/removed from the ingredient list
    private async void OnCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        //TODO: Add/remove ingredient to item list
        if (e.Value)
        {
            //ADD INGREDIENT TO LIST
        }
        else; //REMOVE INGREDIENT FROM LIST
    }
}