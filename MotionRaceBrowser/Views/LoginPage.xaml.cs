using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotionRaceBrowser.Constant;
using MotionRaceBrowser.Service;
using MotionRaceBrowser.ViewModels;
using Xamarin.Forms;
#if __ANDROID__
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
#endif

namespace MotionRaceBrowser.Views
{
    public partial class LoginPage : ContentPage
    {
        Strings stringInstance;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageModel();
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
            loginButton.Text = stringInstance.Login;
            signupButton.Text = stringInstance.NewParticipant;

#if MotionsRace
            welcomeToLbl.Text = stringInstance.WelcomeToRace;
#else
            //welcomeToLbl.Text = stringInstance.WelcomeToTwitch;
            welcomeToLbl.IsVisible = false;
#endif

#if DEBUG
            email.Text = "Berit2";
            password.Text = "123";
#endif
            email.ReturnCommand = new Command(() => password.Focus());
            password.ReturnCommand = new Command(() => OnLoginButtonClicked(null, null));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
#if __ANDROID__
            AskPermission();
#endif
        }

        async void OnLoginButtonClicked(object sender, EventArgs args)
        {
            await LoginAsync(email.Text, password.Text);
        }

        void OnSignupButtonClicked(object sender, EventArgs args)
        {
            //Device.OpenUri(new Uri(Constants.SIGNUP));
            Navigation.PushAsync(new RegisterPage());
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

#if __ANDROID__
        async void AskPermission()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        await DisplayAlert("Need Camera permission", "", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    if (results.ContainsKey(Permission.Camera))
                        status = results[Permission.Camera];
                }

                if (status == PermissionStatus.Granted)
                {

                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Camera Denied", "", "OK");
                }
            }
            catch (Exception ex)
            {
            }
        }
#endif
    }
}
