namespace Recipes.Services;

public static class AnnouncementHelper
{
    public static async Task Announce(string recipeName)
    {
#if IOS
        var _semanticScreenReader = DependencyService.Get<IAsyncAnnouncement>();
        await _semanticScreenReader.AnnounceAsync(recipeName);
#else
        SemanticScreenReader.Announce(recipeName);
#endif
    }
}
