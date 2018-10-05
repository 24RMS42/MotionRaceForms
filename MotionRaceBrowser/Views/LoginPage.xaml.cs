using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotionRaceBrowser.Constant;
using Xamarin.Forms;

namespace MotionRaceBrowser.Views
{
    public partial class LoginPage : ContentPage
    {
        Strings stringInstance;
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            stringInstance = new Strings();
            email.Placeholder = stringInstance.UserName;
            password.Placeholder = stringInstance.Password;
            welcomeToLbl.Text = stringInstance.WelcomeToRace;
            loginButton.Text = stringInstance.Login;
        }

        async void OnLoginButtonClicked(object sender, EventArgs args)
        {
            await LoginAsync(email.Text, password.Text);
        }

        private bool CheckValidate()
        {
            if (string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(password.Text))
            {
                DisplayAlert(stringInstance.Warning, stringInstance.EmailPasswordRequire, "OK");
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
