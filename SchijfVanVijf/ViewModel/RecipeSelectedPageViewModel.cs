using System.ComponentModel;
using System.Windows.Input;
using SchijfVanVijf.Models;
using SchijfVanVijf.Data;
using System.Collections.ObjectModel;

namespace SchijfVanVijf.ViewModel;

public class RecipeSelectedPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand LessCommand { get; }
    public ICommand MoreCommand { get; }
    public Recipe Recipe { get; set; }
    public ObservableCollection<string> Ingredients { get; set; } = new();
    private readonly Database _database;
    private string _ingredienttext;
    public string ingredienttext
    {
        get => _ingredienttext;
        set
        {
            if (_ingredienttext != value)
            {
                _ingredienttext = value;
                OnPropertyChanged(nameof(ingredienttext));
            }
        }
    }
    private List<Ingredient> ingredients;
    private List<RecipeIngredient> recipeingredient;
    private double _servingsAmount;
    public double servingsAmount
    {
        get => _servingsAmount;
        set
        {
            if (_servingsAmount != value)
            {
                _servingsAmount = value;
                OnPropertyChanged(nameof(servingsAmount));
            }
        }
    }

    private string _servingsText;
    public string servingsText
    {
        get => _servingsText;
        set
        {
            if (_servingsText != value)
            {
                _servingsText = value;
                OnPropertyChanged(nameof(servingsText));
            }
        }
    }
    public RecipeSelectedPageViewModel(Recipe recipe)
    {
        Recipe = recipe;
        servingsAmount = 4;
        _servingsText = $"{servingsAmount} servings";
        LessCommand = new Command(LessServings);
        MoreCommand = new Command(MoreServings);
        _database = IPlatformApplication.Current.Services.GetRequiredService<Database>();
        LoadListsAsync();
    }

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    //TODO: Write OnScrollViewScrolled - Used to scroll through longer recipes
    //public async Task OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    //{
    //}

    public async void LessServings()
    {
        servingsAmount--;
        if (servingsAmount < 1)
        {
            servingsAmount = 0.5;
        }
        servingsText = $"{servingsAmount} servings";
        DisplayLists();
    }

    public async void MoreServings()
    {
        servingsAmount++;
        if (servingsAmount == 1.5)
        {
            servingsAmount = 1;
        }
        servingsText = $"{servingsAmount} servings";
        DisplayLists();
    }

    //Loads the lists from the database
    public async void LoadListsAsync()
    {
        ingredients = await _database.GetIngredientListForRecipe(Recipe.Recipe_Id); //Gets all of the ingredient items
        recipeingredient = await _database.GetRecipeIngredientList(Recipe.Recipe_Id); //Gets the corresponding recipeingredient items
        DisplayLists();
    }

    //Displays the ingredients with their amount on a label in the Recipe Selected Page
    public async void DisplayLists()
    {
        Ingredients.Clear(); //Clears out previous items
        foreach (Ingredient ingredient in ingredients)
        {
            foreach (RecipeIngredient ri in recipeingredient)
            {
                if (ingredient.Ingredient_Id == ri.Ingredient_Id) //if the ingredient ID's are the same, the amount of an ingredient is adjused based on the serving amount
                {
                    //TODO: Fix long numbers and add measurements
                    ingredienttext = $"{(ri.Amount) * (servingsAmount / Recipe.Servings)} {ingredient.Name}";
                    Ingredients.Add(ingredienttext);
                }
            }
        }
    }
}