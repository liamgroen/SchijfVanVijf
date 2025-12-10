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
    public ObservableCollection<Ingredient> Ingredients { get; set; } = new();
    private readonly Database _database;
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
        //TODO: Split view into view and viewmodel

        Recipe = recipe;
        servingsAmount = 4;
        _servingsText = $"{servingsAmount} servings";
        LessCommand = new Command(LessServings);
        MoreCommand = new Command(MoreServings);
        _database = IPlatformApplication.Current.Services.GetRequiredService<Database>();
        LoadIngredientsAsync();
    }

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    //TODO: Write OnScrollViewScrolled - Used to scroll through longer recipes
    public async Task OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
    }

    public async void LessServings()
    {
        servingsAmount--;
        if (servingsAmount < 1)
        {
            servingsAmount = 0.5;
        }
        servingsText = $"{servingsAmount} servings";
    }

    public async void MoreServings()
    {
        servingsAmount++;
        if (servingsAmount == 1.5)
        {
            servingsAmount = 1;
        }
        servingsText = $"{servingsAmount} servings";
    }

    public async void LoadIngredientsAsync()
    {
        var ingredients = await _database.GetIngredientListForRecipe(Recipe.Recipe_Id);
        foreach (Ingredient ingredient in ingredients)
        {
            Ingredients.Add(ingredient);
        }
    }
}