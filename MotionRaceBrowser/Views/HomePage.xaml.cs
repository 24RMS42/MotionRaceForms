﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MotionRaceBrowser.Constant;
using MotionRaceBrowser.Interface;
using MotionRaceBrowser.Service;
using Xamarin.Forms;
using ZXing;

namespace MotionRaceBrowser.Views
{
    public partial class HomePage : ContentPage
    {
        string requestUrl;
        Strings stringInstance;
        private bool _isRunning;

        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            stringInstance = new Strings();
            scanQRBtn.Text = stringInstance.ScanQR;
            myQRBtn.Text = stringInstance.MyQR;
            homeBtn.Text = stringInstance.Start;
            goBackBtn.Text = stringInstance.GoBack;
            logoutBtn.Text = stringInstance.Logout;
            searchBtn.Text = stringInstance.Search;
            searchQuery.Placeholder = stringInstance.SearchParticipantOrActivity;

#if __IOS__
            openBrowserBtn.Text = stringInstance.OpenInSafari;
#else
            openBrowserBtn.Text = stringInstance.OpenInBrowser;
#endif

            RequestLogin();

            MessagingCenter.Subscribe<App>(this, "relogin", (sender) =>
            {
                Console.WriteLine("go to auto login");
                OnLockButtonClicked(null, null);
            });
            MessagingCenter.Subscribe<MessageServiceClass, string>(this, "searchDone", (sender, arg) =>
            {
                SearchPerform();
            });

            searchQuery.ReturnCommand = new Command(() => SearchPerform());

            int targetAreaWidth = Device.Idiom == TargetIdiom.Tablet ? 500 : 250;
            zxingView.WidthRequest = App.ScreenWidth;
            zxingView.HeightRequest = App.ScreenHeight - 60;

#if __ANDROID__
            double widthOffset = 0.135;
            double heightOffset = 0.09;
#else
            double widthOffset = 0.16;
            double heightOffset = 0.30;
#endif

            zxingRelativeLayout.Children.Add(targetImageView, Constraint.RelativeToParent((parent) =>
            {
                return parent.Width * widthOffset;
            }), Constraint.RelativeToParent((parent) =>
            {
                return parent.Height * heightOffset;
            }), Constraint.RelativeToParent((parent) =>
            {
                return targetAreaWidth;
            }), Constraint.RelativeToParent((parent) =>
            {
                return targetAreaWidth;
            }));
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            HideMenu();
        }

        void OnWebviewNavigating(object sender, WebNavigatingEventArgs e)
        {
            _isRunning = true;
            Device.StartTimer(new TimeSpan(0, 0, 8), () =>
            { //need to hide loading after 8 seconds
                Console.WriteLine("test:" + _isRunning);
                if (_isRunning)
                {
                    _isRunning = false;
                    loadingView.IsVisible = false;
                }
                return _isRunning;
            });

            loadingView.IsVisible = true;
            var url = e.Url;
            Debug.WriteLine("request url:", url);
            if (url.Contains(Constants.LOGIN_PATTERN))
            {
                OnLockButtonClicked(null, null);
            }

            if (url.EndsWith(".pdf", StringComparison.Ordinal))
            {
                Device.OpenUri(new Uri(url));
            }
        }

        void OnWebviewEndNavigating(object sender, WebNavigatedEventArgs e)
        {
            _isRunning = false;
            loadingView.IsVisible = false;
        }

        void OnBackButtonClicked(object sender, EventArgs e)
        {
            OnGGTapped(null, null);
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
        }

        void OnHomeButtonClicked(object sender, EventArgs e)
        {
            OnGGTapped(null, null);
            webView.Source = App.BaseUrl;
        }

        void OnRefreshButtonClicked(object sender, EventArgs e)
        {
            OnGGTapped(null, null);
            webView.Source = (webView.Source as UrlWebViewSource).Url;
        }

        void OnOpenBrowserButtonClicked(object sender, EventArgs e)
        {
            OnGGTapped(null, null);

            var currentPageUrl = (webView.Source as UrlWebViewSource).Url;
            Uri uri = new Uri(currentPageUrl);
            var baseUrl = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
            var requestedUrl = currentPageUrl.Substring(baseUrl.Length, currentPageUrl.Length - baseUrl.Length);
            requestedUrl = System.Web.HttpUtility.UrlEncode(requestedUrl);

            var deviceLanguage = System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag;
            requestUrl = App.BaseUrl + "applogin.aspx?applicationid=" + Constants.ApplicaitonId.ToLower() + "&loginid=" + App.LoginId + "&ticket=" + App.HashedSecret + "&language=" + deviceLanguage + "&wantedURL=" + requestedUrl;
            Device.OpenUri(new Uri(requestUrl));
        }

        void OnMenuButtonClicked(object sender, EventArgs e)
        {
            VerticalSlideEffect.SetIsShown(menuView, !VerticalSlideEffect.GetIsShown(menuView));
        }

        void OnBarcodeBtnClicked(object sender, EventArgs e)
        {
            HideMenu();
            VerticalSlideEffect.SetIsShown(searchBoxView, false);
            ToggleBarcodeScanView();
        }

        void OnMyQRClicked(object sender, EventArgs e)
        {
            OnGGTapped(null, null);
            webView.Source = App.BaseUrl + Constants.MY_QR;
        }

        void OnSearchBtnClicked(object sender, EventArgs e)
        {
            HideMenu();
            ToggleSearchBox();

            if (VerticalSlideEffect.GetIsShown(barcodeScanView))
            {
                ToggleBarcodeScanView();
            }
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            OnGGTapped(null, null);
            Application.Current.Properties.Clear();
            DependencyService.Get<IClearCookies>().Clear();
            webView.Source = App.BaseUrl + Constants.LOGOUT;
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
            OnGGTapped(null, null);
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

        void OnRegisterClicked(object sender, EventArgs e)
        {
            webView.Source = App.BaseUrl + Constants.REGISTER_NEW_ACTIVITY;
        }

        void OnBackArrowClicked(object sender, EventArgs e)
        {
            ToggleSearchBox();
        }

        void RequestLogin()
        {
            var deviceLanguage = System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag;
            Console.WriteLine("device:" + deviceLanguage);
            requestUrl = App.BaseUrl + "applogin.aspx?applicationid=" + Constants.ApplicaitonId.ToLower() + "&loginid=" + App.LoginId + "&ticket=" + App.HashedSecret + "&language=" + deviceLanguage;
            webView.Source = requestUrl;
        }

        /*
         * Hide any view
         */
        void OnGGTapped(object sender, EventArgs e)
        {
            if (VerticalSlideEffect.GetIsShown(menuView))
            {
                HideMenu();
            }

            if (VerticalSlideEffect.GetIsShown(searchBoxView))
            {
                ToggleSearchBox();
            }

            if (VerticalSlideEffect.GetIsShown(barcodeScanView))
            {
                ToggleBarcodeScanView();
            }
        }

        void HideMenu()
        {
            VerticalSlideEffect.SetIsShown(menuView, false);
        }

        void ToggleSearchBox()
        {
            VerticalSlideEffect.SetIsShown(searchBoxView, !VerticalSlideEffect.GetIsShown(searchBoxView));
            if (VerticalSlideEffect.GetIsShown(searchBoxView))
            {
                searchQuery.Focus();
                searchQuery.Text = string.Empty;
            }
        }

        void ToggleBarcodeScanView()
        {
            VerticalSlideEffect.SetIsShown(barcodeScanView, !VerticalSlideEffect.GetIsShown(barcodeScanView));
            if (VerticalSlideEffect.GetIsShown(barcodeScanView))
            {
                zxingView.IsScanning = true;
            }
            else
            {
#if __IOS__
                zxingView.IsScanning = false;
#endif
            }
        }

        void OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Debug.WriteLine("qr result", result.Text);
                OnGGTapped(null, null);
                webView.Source = result.Text;
            });
        }

        void SearchPerform()
        {
            OnGGTapped(null, null);
            if (!String.IsNullOrEmpty(searchQuery.Text))
            {
                webView.Source = App.BaseUrl + Constants.SEARCH_CAR + searchQuery.Text;
                searchQuery.Text = string.Empty;
            }
        }

        public async Task ShowMessage(string title,
                                      string message,
                                      string buttonText,
                                      Action afterHideCallback)
        {
            await DisplayAlert(title, message, buttonText);

            afterHideCallback?.Invoke();
        }
    }
}
