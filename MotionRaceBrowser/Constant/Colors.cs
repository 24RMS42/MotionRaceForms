namespace MotionRaceBrowser.Constant
{
    public static class Colors
    {

#if MotionsRace
        public static string PrimaryColor = "#163e55";
        public static string SecondaryColor = "#0000FF";
        public static string MenuColor = "#C0C0C0";
        public static string BottomBarColor = "#C0C0C0";
#elif Twitch
        public static string PrimaryColor = "#42d06e";
        public static string SecondaryColor = "#000";
        public static string MenuColor = "#42d06e";
        public static string BottomBarColor = "#C0C0C0";
#else
        public static string PrimaryColor = "#83b81a";
        public static string SecondaryColor = "#0000FF";
        public static string MenuColor = "#83b81a";
        public static string BottomBarColor = "#C0C0C0";
#endif
    }
}
