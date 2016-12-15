using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class VoucherContainer
    {
        /// <summary>
        /// 本期发生明细
        /// </summary>
        public List<VoucherInfo> VoucherList { get; set; }
        /// <summary>
        /// 收款明细
        /// </summary>
        public List<CollectionDetailInfo> CollectionDetails { get; set; }
        /// <summary>
        /// 汇总
        /// </summary>
        public VoucherSummaryInfo VoucherSummary { get; set; }
    }
}