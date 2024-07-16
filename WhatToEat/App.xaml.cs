using Recipes.Services;

namespace Recipes;

public partial class App : Microsoft.Maui.Controls.Application
{

    public App()
    {
			InitializeComponent();
        DependencyService.Register<MockDataStore>();
        DependencyService.Register<IAsyncAnnouncement, SemanticScreenReaderAsyncImplementation>();
			MainPage = new AppShell();
		}
}
