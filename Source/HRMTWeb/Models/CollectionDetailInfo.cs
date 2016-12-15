using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class CollectionDetailInfo
    {
        /// <summary>
        /// 交易日
        /// </summary>
        public DateTime TradingDay { get; set; }
        /// <summary>
        /// 单据类型
        /// </summary>
        public string BillType { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}