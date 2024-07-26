using Microsoft.Maui.Controls.PlatformConfiguration;
using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Recipes.Services;

public partial class KeyboardSelectBehavior : PlatformBehavior<VisualElement, Android.Views.View>
{
    protected override void OnAttachedTo(VisualElement element, Android.Views.View platformView)
    {
        base.OnAttachedTo(element, platformView);
    }

    protected override void OnDetachedFrom(VisualElement element, Android.Views.View platformView)
    {
        base.OnDetachedFrom(element, platformView);
    }
}
