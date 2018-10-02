using System.Collections.Generic;
using MotionRaceBrowser.Network;
using MotionRaceBrowser.Views;
using Xamarin.Forms;

namespace MotionRaceBrowser
{
    public partial class App : Application
    {
        public static HttpHandler G_HTTP_CLIENT { get; set; }
        public static string Token { get; set; }
        public static string BaseUrl { get; set; }

        public App()
        {
            InitializeComponent();

            G_HTTP_CLIENT = new HttpHandler();

            IDictionary<string, object> properties = Current.Properties;
            if (properties.ContainsKey("baseUrl"))
            {
                BaseUrl = properties["baseUrl"].ToString();
                MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
