using System;
using Foundation;
using InternalCars.iOS.Helpers;
using MotionRaceBrowser.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(IClearCookiesImplementation))]
namespace InternalCars.iOS.Helpers
{
    public class IClearCookiesImplementation : IClearCookies
    {
        public void Clear()
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in CookieStorage.Cookies)
                CookieStorage.DeleteCookie(cookie);
        }
    }
}
