using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotionRaceBrowser.Constant;
using MotionRaceBrowser.Service;
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

            //login event from Done button click
            MessagingCenter.Subscribe<MessageServiceClass, string>(this, "login", (sender, arg) =>
            {
                Console.WriteLine("go to login");
                OnLoginButtonClicked(null, null);
            });

            stringInstance = new Strings();
            email.Placeholder = stringInstance.UserName;
            password.Placeholder = stringInstance.Password;
            welcomeToLbl.Text = stringInstance.WelcomeToRace;
            loginButton.Text = stringInstance.Login;
            signupButton.Text = stringInstance.NewParticipant;

#if DEBUG
            email.Text = "erix";
            password.Text = "123";
#endif
            email.ReturnCommand = new Command(() => password.Focus());
            password.ReturnCommand = new Command(() => OnLoginButtonClicked(null, null));
        }

        async void OnLoginButtonClicked(object sender, EventArgs args)
        {
            await LoginAsync(email.Text, password.Text);
        }

        void OnSignupButtonClicked(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri(Constants.SIGNUP));
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
