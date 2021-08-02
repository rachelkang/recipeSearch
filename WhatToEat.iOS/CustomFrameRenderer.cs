using Foundation;
using Recipes;
using Recipes.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace Recipes.iOS
{
    public class CustomFrameRenderer : FrameRenderer
    {
        protected override async void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            NativeView.AccessibilityTraits |= UIAccessibilityTrait.Button;

            var things = NativeView.AccessibilityLabel;
            await Task.Delay(4000);
            things = NativeView.AccessibilityLabel;
        }
    }
}