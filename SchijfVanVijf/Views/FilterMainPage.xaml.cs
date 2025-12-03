using Microsoft.Maui.Platform;
using SchijfVanVijf.ViewModel;

namespace SchijfVanVijf.Views;

public partial class FilterMainPage : ContentPage
{
	public FilterMainPage()
	{
		InitializeComponent();
		BindingContext = new ViewModel.FilterMainPageViewModel();
    }

    private async void OnButtonClick(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        string text = button.Text;

        await Navigation.PushAsync(page: new Views.FilterSelectedPage(text));
    }
    private async void OkButtonClick(object sender, EventArgs e)
    {
        //TODO: Hand back list of selected ingredients (max 5?) which will be filled in in the entry boxes
        await Navigation.PopAsync();
    }
}