using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class VoucherInfo
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
        /// 型号
        /// </summary>
        public string ModelNo { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string BatchNo { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }        
        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}