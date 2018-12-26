using System;
namespace MotionRaceBrowser.Constant
{
    public static class Constants
    {

#if MotionsRace
        public static string BASE = "https://app.motionsrace.com/applogin.aspx";
        public static string SIGNUP = "https://app.motionsrace.com/signup/index.aspx";
        #if __IOS__
        public static string ApplicaitonId = "237C451B-D1AC-41AF-A8FB-C8480C8A6B05";
        public static string ApplicaitonSecret = "CD026EAD-C735-46FA-840E-98040E74CAC5";
        #else
        public static string ApplicaitonId = "FF89F2F6-EECE-4E84-AB65-59BA8D1F33FD";
        public static string ApplicaitonSecret = "BDB4063F-127E-4158-B521-E9469EB2C35E";
        #endif
#else
        public static string BASE = "https://twitch.motionsrace.com/applogin.aspx";
        public static string SIGNUP = "https://twitch.motionsrace.com/signup/index.aspx";
        #if __IOS__
        public static string ApplicaitonId = "C0DB2193-B5B6-4EB5-8178-E528DE060823";
        public static string ApplicaitonSecret = "256DD42E-6841-4591-987C-69E4FA01BE7C";
        #else
        public static string ApplicaitonId = "723A42DA-4BD4-498C-8B15-67F62FACD7D2";
        public static string ApplicaitonSecret = "C9C2E97A-EBE6-4E71-8FB5-B439AC94ACF4";
        #endif
#endif

        public static string LOGIN_PATTERN = "/login.aspx";
        public static string LOGOUT = "logout.aspx";
        public static string SEARCH_CAR = "welcome.aspx?searchText=";
        public static string MY_QR = "person/myqr.aspx";
    }
}
