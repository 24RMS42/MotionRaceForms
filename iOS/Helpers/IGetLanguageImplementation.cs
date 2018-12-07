using System;
using Foundation;
using MotionRaceBrowser.Interface;
using MotionRaceBrowser.iOS.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(IGetLanguageImplementation))]
namespace MotionRaceBrowser.iOS.Helpers
{
    public class IGetLanguageImplementation : IGetLanguage
    {
        public bool IsSwedish()
        {
            var deviceLanguage = NSLocale.PreferredLanguages[0];
            return deviceLanguage.Contains("sv");
        }
    }
}
