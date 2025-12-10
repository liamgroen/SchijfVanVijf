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





	public async void OnScrollViewScrolled (Object sender, ScrolledEventArgs e)
	{   
    }


}