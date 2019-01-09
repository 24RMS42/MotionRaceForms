using System;
using System.Collections.Generic;
using System.Diagnostics;
using MotionRaceBrowser.Constant;
using Xamarin.Forms;

namespace MotionRaceBrowser.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            webView.Source = Constants.SIGNUP;
        }

        void OnWebviewNavigating(object sender, WebNavigatingEventArgs e)
        {
        }

        void OnWebviewEndNavigating(object sender, WebNavigatedEventArgs e)
        {
        }

        void CancelClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void DoneClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
