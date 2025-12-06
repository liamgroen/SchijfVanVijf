using System.Collections.ObjectModel;

namespace SchijfVanVijf.Views;

public partial class RecipeMainPage : ContentPage
{
    //Creates a observableCollection for the matching recipes
    public ObservableCollection<RecipeItems> RecipeList { get; set; } = new ObservableCollection<RecipeItems>();
    public class RecipeItems
    {
        public string RecipeName {get; set;}
    }
    public RecipeMainPage()
	{
		InitializeComponent();
        BindingContext = this;
        //Adds a few recipes to the recipelist for testing
        RecipeList.Add(new RecipeItems { RecipeName = "Hamburger" });
        RecipeList.Add(new RecipeItems { RecipeName = "Fish and chips" });
        RecipeList.Add(new RecipeItems { RecipeName = "Tomato soup" });
        RecipeList.Add(new RecipeItems { RecipeName = "Sushi" });
        RecipeList.Add(new RecipeItems { RecipeName = "Pancakes" });
        RecipeList.Add(new RecipeItems { RecipeName = "Ramen" });
        RecipeList.Add(new RecipeItems { RecipeName = "Spaghetti" });
        RecipeList.Add(new RecipeItems { RecipeName = "Chicken wings" });
        RecipeList.Add(new RecipeItems { RecipeName = "Scones" });
        RecipeList.Add(new RecipeItems { RecipeName = "Brownies" });
        RecipeList.Add(new RecipeItems { RecipeName = "Shepherd's pie" });
        RecipeList.Add(new RecipeItems { RecipeName = "Spareribs" });

    }
    //Checks the text of the button that the user has pressed and hands this to the newly created recipe selected page
    private async void OnButtonClick(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        string text = button.Text;

        await Navigation.PushAsync(page: new Views.RecipeSelectedPage(text));
    }
}