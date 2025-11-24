namespace SchijfVanVijf;

public partial class App : Application
{
	public App(MainPage mainPage)
	{
		InitializeComponent();

		MainPage = mainPage; // Use the injected MainPage
    }
}
