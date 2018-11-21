using System;
using Xamarin.Forms;
using System.Net;
using Android.Webkit;
using MotionRaceBrowser.Interface;
using MotionRaceBrowser.Droid.Helpers;

[assembly: Dependency(typeof(IClearCookiesImplementation))]
namespace MotionRaceBrowser.Droid.Helpers
{
    public class IClearCookiesImplementation : IClearCookies
    {
        public void Clear()
        {
            var cookieManager = CookieManager.Instance;
            cookieManager.RemoveAllCookie();
        }
    }
}
