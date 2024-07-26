using Microsoft.Maui.Controls.PlatformConfiguration;
using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using UIKit;

namespace Recipes.Services;

public partial class KeyboardSelectBehavior : PlatformBehavior<VisualElement, UIView>
{
    protected override void OnAttachedTo(VisualElement element, UIView platformView)
    {
        base.OnAttachedTo(element, platformView);
    }

    protected override void OnDetachedFrom(VisualElement element, UIView platformView)
    {
        base.OnDetachedFrom(element, platformView);
    }
}
