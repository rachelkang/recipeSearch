using Microsoft.Maui.Controls.PlatformConfiguration;
using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace Recipes.Services;

public partial class KeyboardSelectBehavior : PlatformBehavior<VisualElement, UIElement>
{
    KeyEventHandler _keyEventHandler;

    protected override void OnAttachedTo(VisualElement element, UIElement platformView)
    {
        base.OnAttachedTo(element, platformView);

        if (element is CarouselView carouselView)
        {
            _keyEventHandler = (s, args) => OnCarouselKeyDown(carouselView, args);
            platformView.PreviewKeyDown += _keyEventHandler;
        }
    }

    protected override void OnDetachedFrom(VisualElement element, UIElement platformView)
    {
        base.OnDetachedFrom(element, platformView);

        if (element is CarouselView carouselView)
        {
            platformView.PreviewKeyDown -= _keyEventHandler;
        }
    }

    void OnCarouselKeyDown(VisualElement element, KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Enter || e.Key == Windows.System.VirtualKey.Space)
        {
            if (element is CarouselView carouselView && carouselView?.CurrentItem is Item item)
            {
                Command?.Execute(item);
            }
            e.Handled = true;
        }
    }
}
