using Microsoft.Maui.Platform;
using SchijfVanVijf.ViewModel;
using System.Collections.ObjectModel;

namespace SchijfVanVijf.Views;

public partial class FilterMainPage : ContentPage
{
    //Creates a observableCollection for the ingredient categories
    public ObservableCollection<CategoryItems> CategoryList { get; set; } = new ObservableCollection<CategoryItems>();
    public class CategoryItems
    {
        public string CategoryName { get; set; }
    }
    public FilterMainPage()
	{
		InitializeComponent();
        //BindingContext = new ViewModel.FilterMainPageViewModel();
        BindingContext = this;
        //Adds a few categories to the categorylist for testing
        CategoryList.Add(new CategoryItems { CategoryName = "groenten" });
        CategoryList.Add(new CategoryItems { CategoryName = "fruit" });
        CategoryList.Add(new CategoryItems { CategoryName = "vlees" });
        CategoryList.Add(new CategoryItems { CategoryName = "vis" });
        CategoryList.Add(new CategoryItems { CategoryName = "vegetarisch" });
        CategoryList.Add(new CategoryItems { CategoryName = "zuivel" });
        CategoryList.Add(new CategoryItems { CategoryName = "ei" });
        CategoryList.Add(new CategoryItems { CategoryName = "pasta" });
        CategoryList.Add(new CategoryItems { CategoryName = "rijst" });
        CategoryList.Add(new CategoryItems { CategoryName = "granen" });
        CategoryList.Add(new CategoryItems { CategoryName = "kruiden" });
        CategoryList.Add(new CategoryItems { CategoryName = "noten" });
        CategoryList.Add(new CategoryItems { CategoryName = "overigen" });
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