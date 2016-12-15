using OppenIT.Core.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace HRMTWeb
{
    public static class WebInitializer
    {
        static string _mainConnectionString;
        public static string MainConnectionString
        {
            get { return _mainConnectionString; }
        }
        public static void Init()
        {
            ConnectionStringSettings mainConnectionSettings = WebConfigurationManager.ConnectionStrings[AppConstants.DbPair.Main];
            _mainConnectionString = mainConnectionSettings.ConnectionString;
            
            EntityInitializer.RegisterDBContext(AppConstants.DbPair.Main, mainConnectionSettings.ProviderName, mainConnectionSettings.ConnectionString);
        }
    }
}