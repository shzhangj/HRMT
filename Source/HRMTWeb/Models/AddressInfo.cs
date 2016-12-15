using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class AddressInfo
    {
        public int? AddressID { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string AddressText { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactPerson { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string ContactMobile { get; set; }
        /// <summary>
        /// 快递公司ID
        /// </summary>
        public string ExpressCompanyID { get; set; }
        /// <summary>
        /// 快递公司名称
        /// </summary>
        public string ExpressCompanyName { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public int CustoemrID { get; set; }
    }
}