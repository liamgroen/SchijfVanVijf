namespace SchijfVanVijf.Views;

public partial class RecipeSelectedPage : ContentPage
{
    public string Recipe { get; set; }
    public RecipeSelectedPage(string recipe)
	{
		InitializeComponent();
		Recipe = recipe;
		BindingContext = this;
	}

	public async void OnScrollViewScrolled (Object sender, ScrolledEventArgs e)
	{   
    }
}