using SchijfVanVijf.Data;
namespace SchijfVanVijf;
using Microsoft.Extensions.DependencyInjection;

public partial class App : Application
{
    public App(Database database)
    {
        InitializeComponent();
        MainPage = new AppShell();
        _ = database.Init();
    }
}
