using System;
using System.ComponentModel;
using Android.Text;
using MotionRaceBrowser.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(BaseEntryRenderer))]
namespace MotionRaceBrowser.Droid.Renderers
{
    public class BaseEntryRenderer : EntryRenderer
    {
        public BaseEntryRenderer() { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Entry item = this.Element;
            if ((Control != null) & (e.NewElement != null) & item.ReturnType == ReturnType.Search)
            {
                Control.InputType = InputTypes.TextFlagCapCharacters;
            }
        }
    }
}
