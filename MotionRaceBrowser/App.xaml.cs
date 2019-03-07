using System;
using System.Collections.Generic;
using System.Diagnostics;
using MotionRaceBrowser.Constant;
using MotionRaceBrowser.Network;
using MotionRaceBrowser.Util;
using MotionRaceBrowser.Views;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace MotionRaceBrowser
{
    public partial class App : Application
    {
        public static HttpHandler G_HTTP_CLIENT { get; set; }
        public static string Token { get; set; }
        public static string BaseUrl { get; set; }
        public static string LoginId { get; set; }
        public static string HashedSecret { get; set; }
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }

        public App()
        {
            InitializeComponent();

            G_HTTP_CLIENT = new HttpHandler();

            //Apply dynamic theme color
            Current.Resources["PrimaryColor"] = Color.FromHex(Colors.PrimaryColor);
            Current.Resources["SecondaryColor"] = Color.FromHex(Colors.SecondaryColor);
            Current.Resources["MenuColor"] = Color.FromHex(Colors.MenuColor);
            Current.Resources["BottomBarColor"] = Color.FromHex(Colors.BottomBarColor);

            IDictionary<string, object> properties = Current.Properties;
            if (properties.ContainsKey("baseUrl"))
            {
                BaseUrl = properties["baseUrl"].ToString();
                LoginId = properties["loginId"].ToString();
                HashedSecret = properties["hashedSecret"].ToString();
                MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=c7955734-5e47-4d8d-b114-9a8d880f64f1;android=00303ec0-688f-40d2-afc5-e628ad7fcec0", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            Console.WriteLine("app go to sleep...");
            var currentTimestamp = DateTime.Now.MillisecondsTimestamp();
            IDictionary<string, object> properties = Current.Properties;
            properties["timestamp"] = currentTimestamp;
        }

        protected override void OnResume()
        {
            Console.WriteLine("app resumed...");
            long currentTimestamp = DateTime.Now.MillisecondsTimestamp();
            IDictionary<string, object> properties = Current.Properties;
            if (properties.ContainsKey("timestamp"))
            {
                long lastTimestamp = (long)properties["timestamp"];
                long diff = (currentTimestamp - lastTimestamp) / 60;
                if (diff >= 60) // more than 60 minutes
                {
                    MessagingCenter.Send<App>(this, "relogin");
                }
            }
        }
    }
}
