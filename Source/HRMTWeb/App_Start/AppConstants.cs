using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb
{
    public class AppConstants
    {
        public static class DbPair
        {
            public const string Main = "main";
        }
        public static class SessionKey
        {
            public const string User = "User";
            public const int CookieExp = 180;
        }
    }
}