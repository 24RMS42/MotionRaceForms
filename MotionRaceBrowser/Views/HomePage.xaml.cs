using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
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

            var deviceLanguage = System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag;
            requestUrl = App.BaseUrl + "applogin.aspx?applicationid=" + Constants.ApplicaitonId.ToLower() + "&loginid=" + App.LoginId + "&ticket=" + App.HashedSecret + "&language=" + deviceLanguage;
            webView.Source = requestUrl;
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
            webView.Source = requestUrl;
        }

        void OnRefreshButtonClicked(object sender, EventArgs e)
        {
            webView.Source = (webView.Source as UrlWebViewSource).Url;
        }

        void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
