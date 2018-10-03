using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MotionRaceBrowser.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = App.WebViewHTMLSource;
            webView.Source = htmlSource;
        }

        void OnBackButtonClicked(object sender, EventArgs e)
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
            else
            {
                this.Navigation.PopAsync(); // closes the in-app browser view.
            }
        }

        void OnHomeButtonClicked(object sender, EventArgs e)
        {
        }

        void OnRefreshButtonClicked(object sender, EventArgs e)
        {
        }

        void OnLogoutButtonClicked(object sender, EventArgs e)
        {
        }
    }
}
