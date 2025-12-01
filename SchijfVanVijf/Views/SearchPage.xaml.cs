using System.Collections.ObjectModel;

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

    private void OnSearchClicked(object sender, EventArgs e)
    {
        DisplayAlert("Search", $"Searching with {IngredientList.Count} ingredients...", "OK");
    }
}