using Recipes.Models;
using Recipes.Services;

namespace Recipes;

public partial class App : Microsoft.Maui.Controls.Application
{
	public static IServiceProvider ServiceProvider { get; private set; }

    public App()
    {
		InitializeComponent();

		var serviceCollection = new ServiceCollection();
		ConfigureServices(serviceCollection);
		ServiceProvider = serviceCollection.BuildServiceProvider();

		MainPage = new AppShell();
	}

	void ConfigureServices(IServiceCollection services)
	{
		services.AddSingleton<IDataStore<Item>, MockDataStore>();
#if IOS
		services.AddSingleton<IAsyncAnnouncement, SemanticScreenReaderAsyncImplementation>();
#endif
	}
}
