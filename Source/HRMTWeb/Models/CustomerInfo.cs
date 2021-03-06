﻿using HRMTWeb.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class CustomerInfo
    {
        public int CustomerID { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CustomerCode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        public string ContactPerson { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string ContactTel { get; set; }        
        /// <summary>
        /// 当前欠款
        /// </summary>
        public decimal UnPayAmount { get; set; }
        /// <summary>
        /// 未发货金额
        /// </summary>
        public decimal UnfilledOrderAmount { get; set; }
        List<CustomerAccountInfo> _accountList = null;
        public List<CustomerAccountInfo> AccountList
        {
            get
            {
                if (_accountList == null)
                    _accountList = ShoppingCartAction.GetAccountList();
                return _accountList;
            }
            set
            {
                _accountList = value;
            }
        }
    }
}