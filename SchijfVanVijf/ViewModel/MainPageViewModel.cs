using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SchijfVanVijf.Data;

namespace SchijfVanVijf.ViewModel;

public class MainPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private readonly Database _database;
    
    public ObservableCollection<string> RecipeNames { get; } = new();
    
    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
            OnPropertyChanged(nameof(IsNotLoading));
        }
    }
    
    public bool IsNotLoading => !IsLoading;
    
    private bool _hasResults;
    public bool HasResults
    {
        get => _hasResults;
        set
        {
            _hasResults = value;
            OnPropertyChanged(nameof(HasResults));
        }
    }
    
    private string _statusMessage;
    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            _statusMessage = value;
            OnPropertyChanged(nameof(StatusMessage));
        }
    }
    
    public ICommand SearchCommand { get; }
    
    public MainPageViewModel()
    {
        _database = new Database();
        _database.Init().ConfigureAwait(false); //initalizeer de database maar één keer bij opstarten
        SearchCommand = new Command(OnSearchRecipes);
        StatusMessage = "Press the button to search for recipes";
    }
    
    private async void OnSearchRecipes()
    {
        try
        {
            IsLoading = true;
            StatusMessage = "Searching...";
            RecipeNames.Clear();
            HasResults = false;
            
            // Mock ingredient IDs 
            var selectedIngredientIds = new List<int> { 1 };
            
            var recipes = await _database.GetRecipesContainingAnyAsync(selectedIngredientIds);
            
            // Update UI
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                foreach (var recipe in recipes)
                {
                    RecipeNames.Add(recipe.Title);
                }
                
                if (RecipeNames.Count > 0)
                {
                    HasResults = true;
                    StatusMessage = $"Found {RecipeNames.Count} recipe(s)";
                }
                else
                {
                    StatusMessage = "No recipes found";
                }
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error searching recipes: {ex}");
            StatusMessage = $"Error: {ex.Message}";
            
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error", 
                        $"Failed to search recipes: {ex.Message}", 
                        "OK");
                }
            });
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}