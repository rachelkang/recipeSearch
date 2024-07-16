namespace Recipes.Services;

public interface IAsyncAnnouncement
{
    Task AnnounceAsync(string text);
}
