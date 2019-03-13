using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MotionRaceBrowser.ViewModels
{
    public class LoginPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int mainPadding;
        Thickness logoMargin;

        public LoginPageModel()
        {
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                MainPadding = 100;
                LogoMargin = new Thickness(0, 0, 0, 0);
            }
            else
            {
                MainPadding = 0;
                LogoMargin = new Thickness(0, 50, 0, 0);
            }
        }

        public int MainPadding {
            get {  return mainPadding; }
            set { SetProperty(ref mainPadding, value); }
        }

        public Thickness LogoMargin
        {
            get { return logoMargin; }
            set { SetProperty(ref logoMargin, value); }
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
