using System.Diagnostics;
namespace SchijfVanVijf.Views;

public partial class RecipeSelectedPage : ContentPage
{
    public RecipeSelectedPage(string recipe)
	{
		InitializeComponent();
        BindingContext = new ViewModel.RecipeSelectedPageViewModel(recipe);

    }


}