using System.Collections.ObjectModel;
using SchijfVanVijf.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SchijfVanVijf;

public partial class MainPageViewModel : ObservableObject
{
    private readonly Database _db;
    
    public ObservableCollection<string> ResultLabels { get; } =
        new ObservableCollection<string>();
    
    [ObservableProperty]
    private bool resultsVisible;
    
    public MainPageViewModel()
    {
        _db = new Database();
    }
    
    [RelayCommand]
    private async Task SearchAsync()
    {
        try
        {
            await LoadResultsAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in LoadResultsAsync: {ex}");
            await Application.Current.MainPage.DisplayAlert("Error", 
                $"An error occurred: {ex.Message}", "OK");
        }
    }
    
    private async Task LoadResultsAsync()
    {
        ResultLabels.Clear();
        var selectedIngredients = new List<int> { 1 };
        
        for (int i = 0; i < 10; i++)
        {
            ResultLabels.Add($"Recipe {i + 1}");
        }
        
        ResultsVisible = true;
    }
}