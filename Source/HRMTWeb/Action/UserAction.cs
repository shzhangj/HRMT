using OppenIT.Core.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Action
{
    public class UserAction
    {
        public static void UpdatePassword(int userID, string newpwd)
        {
            string sql = "UPDATE c_wldw SET FPwd = @FPwd FROM c_wldw WHERE wldwid = @wldwid";
            string encodingPWD = Utils.MD5(newpwd);
            using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
            {
                int ret = dbc.Execute(sql, new { FPwd = encodingPWD, wldwid = userID });
            }
        }
    }
}