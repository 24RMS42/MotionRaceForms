using System;
using MotionRaceBrowser.Droid.Helpers;
using MotionRaceBrowser.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(IGetLanguageImplementation))]
namespace MotionRaceBrowser.Droid.Helpers
{
    public class IGetLanguageImplementation : IGetLanguage
    {
        public bool IsSwedish()
        {
            var deviceLanguage = System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag;
            return deviceLanguage == "sv-SE";
        }
    }
}
