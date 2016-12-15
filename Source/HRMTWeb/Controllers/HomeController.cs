using HRMTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OppenIT.Core.Framework;
using HRMTWeb.Action;

namespace HRMTWeb.Controllers
{
    [Authentication]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        
        public ActionResult Index()
        {
            return View("StockQuery");
        }
        
        [HttpPost]
        public ActionResult AddressEdit()
        {
            string processType = Request["hidProcessType"];
            string addressIDStr = Request["tbxAddressID"];
            string addressText = Request["tbxAddressText"];
            string contactPerson = Request["tbxContactPerson"];
            string contactTel = Request["tbxContactTel"];
            string contactMobile = Request["tbxContactMobile"];
            string expressCompanyID = Request["ddlExpressCompanyID"];
            int customerID = AppSessionHelper.User.wldwid;
            AddressInfo addr = null;
            string errText = "";
            try
            {
                if (processType == "Query")
                {
                    addr = ShoppingCartAction.GetAddress(int.Parse(addressIDStr));
                }
                else if (processType == "Edit")
                {
                    int? addressID = null;
                    int tmpAddrID;
                    if (int.TryParse(addressIDStr, out tmpAddrID))
                        addressID = tmpAddrID;
                    addr = new AddressInfo()
                    {
                        AddressID = addressID,
                        AddressText = addressText,
                        ContactPerson = contactPerson,
                        ContactTel = contactTel,
                        ContactMobile = contactMobile,
                        ExpressCompanyID = expressCompanyID,
                        CustoemrID = customerID
                    };
                    ShoppingCartAction.UpdateAddress(addr);
                }
            }
            catch (Exception ex)
            {
                errText = ex.Message;
            }
            var retJObj = new { IsSuccess = string.IsNullOrEmpty(errText), ErrText = errText, Address = addr };
            return Json(retJObj);
        }
        public ActionResult AddressModify()
        {
            return View("AddressModify");
        }
        public ActionResult ViewAddressList()
        {
            AddressContainer ac = new AddressContainer(AppSessionHelper.User.wldwid);
            return View("ucAddressView", ac.AddressList);
        }
        public ActionResult ViewAddressSelector()
        {
            AddressContainer ac = new AddressContainer(AppSessionHelper.User.wldwid);
            return View("ucAddressSelector", ac.AddressList);
        }
        public ActionResult ExpressSelector()
        {
            List<ExpressCompanyInfo> ecList = ShoppingCartAction.GetAllExpressCompany();
            return View("ucExpressSelector", ecList);
        }
        [HttpPost]
        public ActionResult DoStockQuery()
        {
            string errText;
            ProductInfo product = null;
            try
            {
                int customerID = AppSessionHelper.User.wldwid;
                string queryKey = Request["QueryKey"];
                using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
                {
                    product = dbc.QueryFirst<ProductInfo>("EXEC p_new_CpKcQuery @wldwid=@CustomerID,@cpCode=@QueryKey", new { CustomerID = customerID, QueryKey = queryKey });
                }
                if (product == null)
                    throw new Exception("未找到该产品!");
            }
            catch (Exception ex)
            {
                errText = ex.Message;
                product = new ProductInfo();
                ViewData["RetMessage"] = errText;
            }
            return View("StockQuery", product);
        }
        [HttpPost]
        public ActionResult DirectBuy(ShoppingCartInfo shoppingCart)
        {
            shoppingCart.wldwid = AppSessionHelper.User.wldwid;
            shoppingCart.FData = DateTime.Now;
            shoppingCart.FAmount = Math.Round(shoppingCart.FPrice * shoppingCart.FQtry, 2, MidpointRounding.AwayFromZero);
            ShoppingCartContainer scc = new ShoppingCartContainer();
            scc.CustomerID = shoppingCart.wldwid;
            scc.ShoppingCartList = new List<ShoppingCartInfo>();
            scc.ShoppingCartList.Add(shoppingCart);
            scc.IsDirectBuy = "1";
            return View("ShoppingCart", scc);
        }
        [HttpPost]
        public ActionResult AddToShopping(ShoppingCartInfo shoppingCart)
        {
            string errText = "";
            try
            {
                shoppingCart.wldwid = AppSessionHelper.User.wldwid;
                shoppingCart.FData = DateTime.Now;
                shoppingCart.FAmount = Math.Round(shoppingCart.FPrice * shoppingCart.FQtry, 2, MidpointRounding.AwayFromZero);
                ShoppingCartAction.InsertShoppingCart(shoppingCart);
            }
            catch (Exception ex)
            {
                errText = ex.Message;
            }
            var retJObj = new { IsSuccess = string.IsNullOrEmpty(errText), ErrText = errText };
            return Json(retJObj);
        }
        public ActionResult StockQuery()
        {
            return View("StockQuery");
        }
        public ActionResult ShoppingCart()
        {
            ShoppingCartContainer scc = new ShoppingCartContainer(AppSessionHelper.User.wldwid);
            scc.IsDirectBuy = "0";
            return View("ShoppingCart", scc);
        }
        [HttpPost]
        public ActionResult ShoppingSubmit(ShoppingCartContainer container)
        {
            string errText;
            try
            {
                bool existsSelected = container.ShoppingCartList.Exists(e => e.selected == "on");
                if (!existsSelected)
                    throw new Exception("请选择明细!");
                if (!container.AddressID.HasValue)
                    throw new Exception("请选择地址!");

                if (!ShoppingCartAction.CreateOrder(container, AppSessionHelper.User.ecode, out errText))
                    throw new Exception(errText);
            }
            catch (Exception ex)
            {
                errText = ex.Message;
            }
            var retJObj = new { IsSuccess = string.IsNullOrEmpty(errText), ErrText = errText, ReloadURL = container.IsDirectBuy == "1" ? "/Home/StockQuery" : "/Home/ShoppingCart" };
            return Json(retJObj);
        }
        [HttpPost]
        public ActionResult DeleteShoppingCart()
        {
            string errText = "";
            try
            {
                string gwcIDStr = Request["gwcid"];
                int gwcid;
                if (!int.TryParse(gwcIDStr, out gwcid))
                    throw new Exception("购物车ID无效!");

                ShoppingCartAction.DeleteShoppingCart(null, gwcid);
            }
            catch (Exception ex)
            {
                errText = ex.Message;
            }
            var retJObj = new { IsSuccess = string.IsNullOrEmpty(errText), ErrText = errText };
            return Json(retJObj);
        }
        public ActionResult VoucherQuery()
        {
            return View("VoucherQuery");
        }
        [HttpPost]
        public ActionResult DoVoucherQuery()
        {
            List<VoucherInfo> vcList = new List<VoucherInfo>();
            List<CollectionDetailInfo> collectionDetails = new List<CollectionDetailInfo>();
            VoucherSummaryInfo vcSummary = new VoucherSummaryInfo();
            string errText = "";
            try
            {
                int customerID = AppSessionHelper.User.wldwid;
                DateTime fromDate, toDate;
                if (!DateTime.TryParse(Request["FromDate"], out fromDate))
                    throw new Exception("请输入起始日期!");
                if (!DateTime.TryParse(Request["ToDate"], out toDate))
                    throw new Exception("请输入结束日期!");

                using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
                {
                    using (var multi = dbc.QueryMultiple(@"EXEC p_new_Customer_BillQuery
	@CustomerID = @CustomerID,
	@Kssj = @FromDate,
	@jssj = @ToDate", new { CustomerID = customerID, FromDate = fromDate, ToDate = toDate }))
                    {
                        if (!multi.IsConsumed)
                        {
                            vcSummary = multi.ReadFirstOrDefault<VoucherSummaryInfo>();
                            vcList = multi.Read<VoucherInfo>().ToList();
                            collectionDetails = multi.Read<CollectionDetailInfo>().ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errText = ex.Message;
                ViewData["RetMessage"] = errText;
            }
            VoucherContainer vc = new VoucherContainer() { VoucherList = vcList, VoucherSummary = vcSummary, CollectionDetails = collectionDetails };
            return View("VoucherQuery", vc);
        }
        public ActionResult OrderQuery()
        {
            List<OrderInfo> orderList = new List<OrderInfo>();
            try
            {
                int customerID = AppSessionHelper.User.wldwid;
                string searchKey = Request["tbxSearchKeys"];
                orderList = ShoppingCartAction.OrderQuery(customerID, Request["tbxSearchKeys"]);
            }
            catch (Exception ex)
            {
                ViewData["RetMessage"] = ex.Message;
            }
            return View("OrderQuery", new OrderContainer() { OrderList = orderList });
        }
    }
}
