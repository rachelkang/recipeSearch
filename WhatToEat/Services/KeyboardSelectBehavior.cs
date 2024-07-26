using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Recipes.Services;

public partial class KeyboardSelectBehavior
{
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(KeyboardSelectBehavior));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}
