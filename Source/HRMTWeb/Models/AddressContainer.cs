using HRMTWeb.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMTWeb.Models
{
    public class AddressContainer
    {
        protected int CustomerID { get; set; }
        public AddressContainer(int customerID)
        {
            this.CustomerID = customerID;
        }
        List<AddressInfo> _addressList = null;
        public List<AddressInfo> AddressList 
        {
            get
            {
                if (_addressList == null)
                    _addressList = ShoppingCartAction.GetAddressList(this.CustomerID);
                return _addressList;
            }
        }        
    }
}