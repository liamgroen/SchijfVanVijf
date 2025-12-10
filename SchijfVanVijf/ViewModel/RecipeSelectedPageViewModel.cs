using System.ComponentModel;
using System.Windows.Input;

namespace SchijfVanVijf.ViewModel;

public class RecipeSelectedPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand LessCommand { get; }
    public ICommand MoreCommand { get; }
    public string Recipe { get; set; }
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
    public RecipeSelectedPageViewModel(string recipe)
    {
        //TODO: Split view into view and viewmodel

        Recipe = recipe;
        servingsAmount = 4;
        _servingsText = $"{servingsAmount} servings";
        LessCommand = new Command(LessServings);
        MoreCommand = new Command(MoreServings);
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
}