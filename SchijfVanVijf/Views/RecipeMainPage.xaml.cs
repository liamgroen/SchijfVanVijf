namespace SchijfVanVijf.Views;

public partial class RecipeMainPage : ContentPage
{
	public RecipeMainPage()
	{
		InitializeComponent();
	}

    private async void OnButtonClick(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        string text = button.Text;

        await Navigation.PushAsync(page: new Views.RecipeSelectedPage(text));
    }
}