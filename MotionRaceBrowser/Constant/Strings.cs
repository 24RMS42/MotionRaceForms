using System;
using MotionRaceBrowser.Interface;
using Xamarin.Forms;

namespace MotionRaceBrowser.Constant
{
    public class Strings
    {
        bool IsSwedish;

        public Strings()
        {
            IsSwedish = DependencyService.Get<IGetLanguage>().IsSwedish();
        }

        public string Login
        {
            get => IsSwedish ? "LOGGA IN" : "LOGIN";
        }

        public string NewParticipant
        {
            get => IsSwedish ? "Ny deltagare" : "New participant";
        }

        public string UserName
        {
            get => IsSwedish ? "Användarnamn" : "Username";
        }

        public string Password
        {
            get => IsSwedish ? "Lösenord" : "Password";
        }

        public string Warning
        {
            get => IsSwedish ? "Varning!" : "Warning!";
        }

        public string EmailPasswordRequire
        {
            get => IsSwedish ? "Användarnamn och lösenord krävs!" : "Email and password are required!";
        }

        public string LoggingIn
        {
            get => IsSwedish ? "Loggar in..." : "Logging in...";
        }

        public string WelcomeToRace
        {
            get => IsSwedish ? "Välkommen till MotionsRace" : "Welcome to MotionsRace";
        }

        public string AppTokenOutdated
        {
            get => IsSwedish ?
                "Denna app är av en gammal version och måste uppdateras. Var vänlig och uppdatera till den senaste versionen av denna app" :
                "Your app version is outdated and needs to be updated. Please install the most recent update of this app";
        }

        public string InvalidUsernamePassword
        {
            get => IsSwedish ? "Ogiltigt användarnamn eller lösenord" : "Invalid user name or password";
        }

        public string LoginFail
        {
            get => IsSwedish ? "Serverfel vid login" : "Server error upon login";
        }

        public string ScanQR
        {
            get => IsSwedish ? "Läs QR-kod" : "Scan QR";
        }

        public string MyQR
        {
            get => IsSwedish ? "Min QR" : "My QR";
        }

        public string Start
        {
            get => IsSwedish ? "Start" : "Start";
        }

        public string GoBack
        {
            get => IsSwedish ? "Gå tillbaka" : "Go back";
        }

        public string Refresh
        {
            get => IsSwedish ? "Ladda om" : "Refresh";
        }

        public string Logout
        {
            get => IsSwedish ? "Logga ut" : "Logout";
        }

        public string Search
        {
            get => IsSwedish ? "Sök" : "Search";
        }

        public string OpenInSafari
        {
            get => IsSwedish ? "Öppna i Safari" : "Open in Safari";
        }

        public string OpenInBrowser
        {
            get => IsSwedish ? "Öppna i Browser" : "Open in Browser";
        }

        public string SearchParticipantOrActivity
        {
            get => IsSwedish ? "Sök deltagare eller aktivitet" : "Search participant or activity";
        }

        public string InvalidInternalCarsQRCode
        {
            get => IsSwedish ? "Ogiltig InternalCars QR-kod" : "Invalid InternalCars QRCode";
        }
    }
}
