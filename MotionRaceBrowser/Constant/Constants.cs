using System;
namespace MotionRaceBrowser.Constant
{
    public static class Constants
    {
        public static string BASE = "https://app.motionsrace.com/applogin.aspx";

#if __IOS__
        public static string ApplicaitonId = "237C451B-D1AC-41AF-A8FB-C8480C8A6B05";
        public static string ApplicaitonSecret = "CD026EAD-C735-46FA-840E-98040E74CAC5";
#else
        public static string ApplicaitonId = "FF89F2F6-EECE-4E84-AB65-59BA8D1F33FD";
        public static string ApplicaitonSecret = "BDB4063F-127E-4158-B521-E9469EB2C35E";
#endif
    }
}
