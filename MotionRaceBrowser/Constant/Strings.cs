using System;
namespace MotionRaceBrowser.Constant
{
    public class Strings
    {
        bool IsSwedish;

        public Strings()
        {
            var deviceLanguage = System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag;
            IsSwedish = deviceLanguage == "sv-SE";
        }

        public string Login
        {
            get => IsSwedish ? "LOGGA IN" : "LOGIN";
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

        public string InvalidUsernamePassword
        {
            get => IsSwedish ? "Ogiltigt användarnamn eller lösenord" : "Invalid user name or password";
        }

        public string LoginFail
        {
            get => IsSwedish ? "Loginfel" : "Login Failed";
        }
    }
}
