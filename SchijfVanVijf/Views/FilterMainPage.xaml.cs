using Microsoft.Maui.Platform;
using System.Collections.ObjectModel;
using SchijfVanVijf.Data;
using SchijfVanVijf.ViewModel;

namespace SchijfVanVijf.Views;

public partial class FilterMainPage : ContentPage
{
    //Creates a observableCollection for the ingredient categories
    public ObservableCollection<string> CategoryList { get; set; } = new();
    private readonly Database _database;


    public FilterMainPage()
	{
		InitializeComponent();
        //BindingContext = new ViewModel.FilterMainPageViewModel();
        BindingContext = this;
        _database = IPlatformApplication.Current.Services.GetRequiredService<Database>();

        LoadCategoriesAsync();
    }


    // add all food categories to the observablecollection
    private async void LoadCategoriesAsync()
    {
        var categories = await _database.GetAllCategories();
        CategoryList.Clear();
        foreach (string category in categories)
        {
            CategoryList.Add(category);
        }
    }

    //Uses the text on the button that is pressed by the user and hands it to the newly created filter selected page
    private async void OnButtonClick(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        string text = button.Text;
        await Navigation.PushAsync(page: new Views.FilterSelectedPage(text));
    }

    //Pops current page of the stack and navigates back to the search page
    private async void OkButtonClick(object sender, EventArgs e)
    {
        //TODO: Hand back list of selected ingredients (max 5?) which will be filled in in the entry boxes
        await Navigation.PopAsync();
    }
}
