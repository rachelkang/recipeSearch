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

    //protected override void OnAttachedTo(VisualElement element)
    //{
    //    element.Loaded += OnLoaded;
    //    element.Unloaded += OnUnloaded;
    //    base.OnAttachedTo(element);
    //}

    //protected override void OnDetachingFrom(VisualElement element)
    //{
    //    element.Loaded -= OnLoaded;
    //    element.Unloaded -= OnUnloaded;
    //    base.OnDetachingFrom(element);
    //}

    //partial void OnLoaded(object sender, EventArgs e)
    //{
    //}
    //partial void OnUnloaded(object sender, EventArgs e)
    //{
    //}
}