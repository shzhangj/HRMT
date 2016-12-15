using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class ShoppingCartInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int gwcid { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int cpdnid { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string cpcode { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int wldwid { get; set; }
        /// <summary>
        /// 产品中类ID
        /// </summary>
        public int cpzlid { get; set; }
        /// <summary>
        /// 产品中类名称
        /// </summary>
        public string cpzlName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int FQtry { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal FPrice { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal FAmount { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime FData { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public string selected { get; set; }
    }
}