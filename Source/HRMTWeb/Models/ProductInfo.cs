using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class ProductInfo
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public int? cpdnid { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string c_cpdn_ecode { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string c_cpdn_ename { get; set; }
        /// <summary>
        /// 版本ID
        /// </summary>
        public int cpzlid { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string c_cpzl_ecode { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        public string c_cpzl_ename { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }
        /// <summary>
        /// 特殊说明
        /// </summary>
        public string bbRemark { get; set; }
        /// <summary>
        /// 库存数
        /// </summary>
        public string kcsl { get; set; }
        /// <summary>
        /// 已预订数量
        /// </summary>
        public int ydsl { get; set; }
        /// <summary>
        /// 查询显示
        /// </summary>
        public string kcxssm { get; set; }

    }
}