using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class OrderInfo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime TransTime { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderItemInfo> Items { get; set; }       
    }
}