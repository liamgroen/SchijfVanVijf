using SchijfVanVijf.ViewModel;
using System.ComponentModel;

namespace SchijfVanVijf.Views;

public partial class FilterSelectedPage : ContentPage
{
    public string Category { get; set; }
    public FilterSelectedPage(string category)
	{
		InitializeComponent();
        //BindingContext = new ViewModel.FilterSelectedPageViewModel();
        Category = category;
        BindingContext = this;
    }
    private async void OkButtonClick(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    //Checks the bool value of the checkbox and detmines if the corresponding ingredient should be added/removed from the ingredient list
    private async void OnCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        //TODO: Add/remove ingredient to item list
        if (e.Value)
        {
            await Navigation.PopAsync();
            //ADD INGREDIENT TO LIST
        }
        else; //REMOVE INGREDIENT FROM LIST
    }
}