using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class UserInfo
    {
        /// <summary>
        /// 用户ID/客户ID
        /// </summary>
        public int wldwid { get; set; }
        public string ecode { get; set; }
        public string ename { get; set; }
        public string FPwd { get; set; }
    }
}