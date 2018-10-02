using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MotionRaceBrowser.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void OnLoginButtonClicked(object sender, EventArgs args)
        {
            await LoginAsync(email.Text, password.Text);
        }

        private bool CheckValidate()
        {
            if (string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(password.Text))
            {
                DisplayAlert("Warning!", "Email and password are required!", "OK");
                return false;
            }
            else
                return true;
        }

        async Task LoginAsync(string useremail, string userpassword)
        {
            if (CheckValidate())
            {
                var result = await App.G_HTTP_CLIENT.LoginAsync(useremail, userpassword);

                if (result)
                {
                    await Navigation.PushAsync(new HomePage());
                }
            }
        }
    }
}
