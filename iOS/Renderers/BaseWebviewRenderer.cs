using System;
using MotionRaceBrowser.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WebView), typeof(BaseWebviewRenderer))]
namespace MotionRaceBrowser.iOS.Renderers
{
    public class BaseWebviewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var view = Element as WebView;
            if (view == null || NativeView == null)
            {
                return;
            }
            this.ScalesPageToFit = true;
        }
    }
}
