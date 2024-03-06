using Microsoft.Maui.ApplicationModel.DataTransfer;
using System.Reflection;
using System.Windows.Input;

namespace Recipes.ViewModels;

public class AboutViewModel : BaseViewModel
{
    public AboutViewModel()
    {
        Title = "About";
        PackageName = "Microsoft.Maui";
        var mauiAssembly = Assembly.Load($"{PackageName}");

        var fileVersion = (AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(mauiAssembly, typeof(AssemblyFileVersionAttribute), false);
        var informationalVersion = (AssemblyInformationalVersionAttribute)Attribute.GetCustomAttribute(mauiAssembly, typeof(AssemblyInformationalVersionAttribute), false);

        PackageInformationalVersion = informationalVersion.InformationalVersion;
        var info = informationalVersion.InformationalVersion.Split('+');

        if (info.Length > 1)
        {
            PackageVersion = info[0];
            var infos = info[1].Split('-');
            foreach (var item in infos)
            {
                if (item.Contains("azdo."))
                {
                    var cleanValue = item.Replace("azdo.", "").Split('.').First();
                    BuildId = item.Replace("azdo.", "");
                    AzdoUrl = $"https://dev.azure.com/devdiv/DevDiv/_build/results?buildId={BuildId}";
                }
                if (item.Contains("sha."))
                {
                    Sha = item.Replace("sha.", "");
                    ShaUrl = $"https://github.com/dotnet/maui/commits/{Sha}";
                }
            }
        }
        OpenWebCommand = new Command<string>(async p => await Browser.OpenAsync(new Uri(p)));
        GoBackCommand = new Command(async () => await Shell.Current.GoToAsync("../"));
        AboutText = @"Rachel's Recipes is a cross-platform mobile application that uses Xamarin.Forms. It was inspired by all those who are new to cooking (dare I say, first-time chefs! :)), especially during these unprecedented times of quarantining, social distancing, and increased cooking. It is intended to provide a single place to both search for new recipes as well as keep track of personal ones.\n\n
                      This app continues to be work-in-progress and at the moment, its primary purpose is to test the accessibility of .NET MAUI.";
    }

    public string AboutText { get; set; }

    public string PackageName { get; set; }

    public string PackageVersion { get; set; }

    public string PackageInformationalVersion { get; set; }

    public string BuildId { get; set; }

    public string AzdoUrl { get; set; }

    public string Sha { get; set; }

    public string ShaUrl { get; set; }

    public ICommand GoBackCommand { get; }
    public ICommand OpenWebCommand { get; }
}
