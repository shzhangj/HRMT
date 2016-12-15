using HRMTWeb.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMTWeb.Models
{
    public class ShoppingCartContainer
    {
        public int CustomerID { get; set; }
        public ShoppingCartContainer() { }
        public ShoppingCartContainer(int customerID)
        {
            this.CustomerID = customerID;
        }
        List<ShoppingCartInfo> _shoppingCartList = null;
        /// <summary>
        /// 购物车明细
        /// </summary>
        public List<ShoppingCartInfo> ShoppingCartList
        {
            get
            {
                if (_shoppingCartList == null)
                    _shoppingCartList = ShoppingCartAction.GetSoppingCarts(this.CustomerID);
                return _shoppingCartList;
            }
            set { _shoppingCartList = value; }
        }
        public int? AddressID { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// 操作类型:直接购买,购物车
        /// </summary>
        public string IsDirectBuy { get; set; }
        
        decimal? _totalAmount = null;
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount
        {
            get
            {
                if (_totalAmount == null)
                    _totalAmount = ShoppingCartList.Sum(s => s.selected == "on" ? s.FAmount : 0);
                return _totalAmount.Value;
            }
            set { _totalAmount = value; }
        }
    }
}