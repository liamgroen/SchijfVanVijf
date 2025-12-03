using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SchijfVanVijf.Views;
public partial class SearchPage : ContentPage
{
    public class IngredientItem
    {
        public string Value { get; set; }
    }

    //this is the list of  ingredients
    public ObservableCollection<IngredientItem> IngredientList { get; set; } = new ObservableCollection<IngredientItem>();

    public SearchPage()
    {
        InitializeComponent();

        BindingContext = this;

        IngredientList.Add(new IngredientItem { Value = "" });
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        var lastItem = IngredientList.LastOrDefault();

        //checking if the last item isnt empty
        if (lastItem != null && string.IsNullOrWhiteSpace(lastItem.Value))
        {
            DisplayAlert("Info", "Please fill in the last ingredient before adding a new one.", "OK");
            return;
        }

        IngredientList.Add(new IngredientItem { Value = "" });
    }

    private async void OnSearchClicked(object sender, EventArgs e)
    {
        //TODO: Should search database for matching ingredients and hand back a list of ingredients to be shown on the recipe main page
        //await DisplayAlert("Search", $"Searching with {IngredientList.Count} ingredients...", "OK");
        new NavigationPage(new MainPage());
        await Navigation.PushAsync(page: new RecipeMainPage());
    }

    private async void GoToFilterMainPage_Clicked(object sender, EventArgs e)
    {
        new NavigationPage(new MainPage());
        await Navigation.PushAsync(page: new FilterMainPage());
    }
}