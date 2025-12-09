namespace SchijfVanVijf.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new ViewModel.MainPageViewModel();
    }
}

