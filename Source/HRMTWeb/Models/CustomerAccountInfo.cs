using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class CustomerAccountInfo
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string AccountNo { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 开户银行
        /// </summary>
        public string OpenBankName { get; set; }

    }
}