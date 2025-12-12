using System.Collections.ObjectModel;
using SchijfVanVijf.Models;
using SchijfVanVijf.Data;
using SchijfVanVijf.ViewModel;
namespace SchijfVanVijf.Views;

public partial class RecipeSelectedPage : ContentPage
{

    public RecipeSelectedPage(Recipe clickedRecipe)
	{
		InitializeComponent();
		BindingContext = new ViewModel.RecipeSelectedPageViewModel(clickedRecipe);
	}
}