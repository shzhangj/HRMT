using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class OrderItemInfo
    {
        /// <summary>
        /// 型号
        /// </summary>
        public string c_cpdn_ecode { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string c_cpdn_ename { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty { get; set; }
        public decimal Amount 
        {
            get
            {
                return Math.Round(this.Price * this.Qty, 2, MidpointRounding.AwayFromZero);
            }
        }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal Freight { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status { get; set; }
    }
}