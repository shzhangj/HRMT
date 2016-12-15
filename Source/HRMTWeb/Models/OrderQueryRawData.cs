using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class OrderQueryRawData
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string ecode { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime trantime { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int khddid { get; set; }
        /// <summary>
        /// 订单明细ID
        /// </summary>
        public int khddmxID { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string xinghao { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string pinming { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int FQty { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal FPrice { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal zk { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal FAmount { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal Freight { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }        
    }
}