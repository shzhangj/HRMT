using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class VoucherSummaryInfo
    {
        /// <summary>
        /// 期初余额
        /// </summary>
        public decimal InitialBalance { get; set; }
        /// <summary>
        /// 本期发生额
        /// </summary>
        public decimal CurrentPeriodAmount { get; set; }
        /// <summary>
        /// 本期其它
        /// </summary>
        public decimal CurrentPeriodOtherAmount { get; set; }
        /// <summary>
        /// 本期余额
        /// </summary>
        public decimal CurrentPeriodBalance { get; set; }
    }
}