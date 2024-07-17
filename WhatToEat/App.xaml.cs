using Recipes.Services;

namespace Recipes;

public partial class App : Microsoft.Maui.Controls.Application
{

    public App()
    {
			InitializeComponent();
        DependencyService.Register<MockDataStore>();
#if IOS
        DependencyService.Register<IAsyncAnnouncement, SemanticScreenReaderAsyncImplementation>();
#endif
			MainPage = new AppShell();
		}
}
