using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MotionRaceBrowser.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Content.FindByName<Button>("loginButton").Clicked += OnLoginButtonClicked;
        }

        void OnLoginButtonClicked(object sender, EventArgs args)
        {
            //LoginAsync();
        }
    }
}
