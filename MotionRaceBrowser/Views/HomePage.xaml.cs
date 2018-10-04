using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MotionRaceBrowser.Constant;
using Xamarin.Forms;

namespace MotionRaceBrowser.Views
{
    public partial class HomePage : ContentPage
    {
        string requestUrl;

        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            RequestLogin();
        }

        void OnBackButtonClicked(object sender, EventArgs e)
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
        }

        void OnHomeButtonClicked(object sender, EventArgs e)
        {
            webView.Source = App.BaseUrl;
        }

        void OnRefreshButtonClicked(object sender, EventArgs e)
        {
            webView.Source = (webView.Source as UrlWebViewSource).Url;
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            webView.Source = App.BaseUrl + "logout.aspx";
            Application.Current.Properties.Clear();
            await Task.Delay(2000);

            int stackCount = Navigation.NavigationStack.Count;
            if (stackCount == 1)
            {
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        async void OnLockButtonClicked(object sender, EventArgs e)
        {
            IDictionary<string, object> properties = Application.Current.Properties;
            if (properties.ContainsKey("email"))
            {
                var email = properties["email"].ToString();
                var password = properties["password"].ToString();
                var result = await App.G_HTTP_CLIENT.LoginAsync(email, password);
                if (result)
                {
                    RequestLogin();
                }
            }
        }

        void RequestLogin()
        {
            var deviceLanguage = System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag;
            requestUrl = App.BaseUrl + "applogin.aspx?applicationid=" + Constants.ApplicaitonId.ToLower() + "&loginid=" + App.LoginId + "&ticket=" + App.HashedSecret + "&language=" + deviceLanguage;
            webView.Source = requestUrl;
        }
    }
}
