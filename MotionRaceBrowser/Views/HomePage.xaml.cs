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
            webView.Source = App.BaseUrl;
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
    }
}
