using HRMTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Action
{
    public class AppSessionHelper
    {
        const string SESSION_KEY_USER = "User";
        public static UserInfo User
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_KEY_USER] != null)
                    return HttpContext.Current.Session[SESSION_KEY_USER] as UserInfo;
                return null;
            }
            set
            {
                HttpContext.Current.Session[SESSION_KEY_USER] = value;
            }
        }
    }
}