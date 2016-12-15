using Dapper;
using HRMTWeb.Models;
using OppenIT.Core.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HRMTWeb.Action
{
    public class ShoppingCartAction
    {
        /// <summary>
        /// 得到客户的购物车列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static List<ShoppingCartInfo> GetSoppingCarts(int customerID)
        {
            string sql = @"SELECT c.*,p.ename AS cpcode,pt.eName AS cpzlName FROM t_gwc c
	JOIN c_cpdn p ON c.cpdnid = p.cpdnid
	JOIN c_cpzl pt ON p.cpzlid = pt.cpzlid
WHERE c.wldwid=@CustomerID ORDER BY c.FData";
            List<ShoppingCartInfo> shippintCarts = new List<ShoppingCartInfo>();
            using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
            {
                shippintCarts = dbc.Query<ShoppingCartInfo>(sql, new { CustomerID = customerID });
            }
            return shippintCarts;
        }       
        public static List<ExpressCompanyInfo> GetAllExpressCompany()
        {
            string sql = @"SELECT wlxxdnid AS ID,ename AS Name FROM c_wlxxdn
WHERE EXISTS(SELECT 1 FROM t_WebSet_Wl WHERE c_wlxxdn.wlxxdnid = t_WebSet_Wl.wlxxdnid)";
            List<ExpressCompanyInfo> exList = new List<ExpressCompanyInfo>();
            using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
            {
                exList = dbc.Query<ExpressCompanyInfo>(sql);
            }
            return exList;
        }
        public static List<AddressInfo> GetAddressList(int customerID)
        {
            string sql = @"SELECT khshrlbID AS AddressID,
	wldwid AS CustoemrID,
	shAddr AS AddressText,
	shr AS ContactPerson,
	FTel AS ContactTel,
	FMobile AS ContactMobile,
	a.wlxxdnid AS ExpressCompanyID,
	e.ename AS ExpressCompanyName
FROM t_khshrlb a
	JOIN c_wlxxdn e ON a.wlxxdnid = e.wlxxdnid
 WHERE wldwid = @CustomerID";
            List<AddressInfo> addrList = new List<AddressInfo>();
            using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
            {
                addrList = dbc.Query<AddressInfo>(sql, new { CustomerID = customerID });
            }
            return addrList;
        }
        public static AddressInfo GetAddress(int addressID)
        {
            string sql = @"SELECT khshrlbID AS AddressID,
	wldwid AS CustoemrID,
	shAddr AS AddressText,
	shr AS ContactPerson,
	FTel AS ContactTel,
	FMobile AS ContactMobile,
	a.wlxxdnid AS ExpressCompanyID,
	e.ename AS ExpressCompanyName
FROM t_khshrlb a
	JOIN c_wlxxdn e ON a.wlxxdnid = e.wlxxdnid
WHERE khshrlbID = @AddressID";
            AddressInfo addr = null;
            using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
            {
                addr = dbc.QueryFirst<AddressInfo>(sql, new { AddressID = addressID });
            }
            return addr;
        }
        public static void UpdateAddress(AddressInfo addr)
        {
            string sql = "";
            if (addr.AddressID.HasValue)
                sql = @"UPDATE t_khshrlb
	SET shAddr = @AddressText,
	shr = @ContactPerson,
	FTel = @ContactTel,
	FMobile = @ContactMobile,
	wlxxdnid = @ExpressCompanyID
FROM t_khshrlb WHERE khshrlbID = @AddressID";
            else
                sql = @"INSERT INTO t_khshrlb(wldwid,wlxxdnid,shAddr,shr,FTel,FMobile)VALUES(@CustoemrID,@ExpressCompanyID,@AddressText,@ContactPerson, @ContactTel,@ContactMobile)";

            using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
            {
                int ret = dbc.Execute(sql, addr);
            }
        }
        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="shoppingCart"></param>
        public static void InsertShoppingCart(ShoppingCartInfo shoppingCart)
        {
            string sql = "INSERT INTO t_gwc(cpdnid,wldwid,cpzlid,FQtry,FPrice,FAmount,FData,Remark)VALUES(@cpdnid,@wldwid,@cpzlid,@FQtry,@FPrice,@FAmount,@FData,@Remark)";
            using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
            {
                int ret = dbc.Execute(sql, shoppingCart);
            }
        }
        /// <summary>
        /// 删除购物车记录
        /// </summary>
        /// <param name="shoppingCartID"></param>
        public static void DeleteShoppingCart(EntityDBContext dbc, int shoppingCartID)
        {
            int ret = 0;
            string sql = "DELETE t_gwc WHERE gwcid = @ShoppingCartID";
            if (dbc == null)
            {
                using (dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
                {
                    ret = dbc.Execute(sql, new { ShoppingCartID = shoppingCartID });
                }
            }
            else
                ret = dbc.Execute(sql, new { ShoppingCartID = shoppingCartID });
        }
        public static bool CreateOrder(ShoppingCartContainer scContainer,string userCode, out string errText)
        {
            errText = "";
            string sql_create_order = @"INSERT INTO t_khdd(wldwid,wlxxdnid,shAddr,shr,FMobile,FTel,ecode,tranmemo,createby,createtime,
closestatus,estatus,xstatus,trantime)
SELECT a.wldwid,a.wlxxdnid,a.shAddr,a.shr,a.FMobile,a.FTel,@OrderNo,@Remark,@UserCode,Getdate(),
0,1,1,Getdate()
FROM t_khshrlb a WHERE khshrlbID = @khshrlbID
SELECT SCOPE_IDENTITY()",
                sql_create_orderDtl = @"INSERT INTO t_khddmx(khddid,cpzlid,cpdnid,xh,Remark,FQty,FPrice,FAmount,xstatus)
SELECT @khddid,p.cpdnid,c.cpdnid,@SeqID,@Remark,@Qty,@Price,@Amount,1
FROM t_gwc c
	JOIN c_cpdn p ON c.cpdnid = p.cpdnid
	JOIN c_cpzl pt ON p.cpzlid = pt.cpzlid
WHERE c.gwcid = @gwcid";
            string orderNo = DateTime.Now.ToString("yyMMddHHmmss");
            using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
            {
                dbc.BeginTransaction();
                try
                {
                    //插入主记录
                    var khddid = dbc.QueryFirst<int>(sql_create_order, new { khshrlbID = scContainer.AddressID, OrderNo = orderNo, Remark = scContainer.Remark, UserCode = userCode });
                    int seq = 1;
                    var shoppingCartList = scContainer.ShoppingCartList.FindAll(e => e.selected == "on");
                    //插入明细记录
                    foreach (ShoppingCartInfo sc in shoppingCartList)
                    {
                        sc.FAmount = Math.Round(sc.FQtry * sc.FPrice, 2, MidpointRounding.AwayFromZero);
                        dbc.Execute(sql_create_orderDtl, new {
                            khddid = khddid,
                            SeqID = seq,
                            Remark = "",
                            Qty = sc.FQtry,
                            Price = sc.FPrice,
                            Amount = sc.FAmount,
                            gwcid = sc.gwcid
                        });
                        seq++;
                        //删除购物车记录
                        DeleteShoppingCart(dbc, sc.gwcid);
                    }

                    dbc.Commit();
                }
                catch (Exception ex)
                {
                    dbc.Rollback();
                    errText = ex.Message;
                }
            }
            return errText == "";
        }
        public static List<OrderInfo> OrderQuery(int customerID, string queryKey)
        {
            string sql = @"EXEC p_Order_Query
			@CustomerID = @CustomerID,
			@QueryKey = @QueryKey";
            List<OrderQueryRawData> rawDataList = null;
            using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
            {
                rawDataList = dbc.Query<OrderQueryRawData>(sql, new { CustomerID = customerID, QueryKey = queryKey });
            }
            Dictionary<int, OrderInfo> orderDict = new Dictionary<int, OrderInfo>();
            foreach (OrderQueryRawData r in rawDataList)
            {
                OrderInfo o = null;
                if (orderDict.ContainsKey(r.khddid))
                    o = orderDict[r.khddid];
                else
                {
                    o = new OrderInfo() { OrderNo = r.ecode, Amount = 0, Items = new List<OrderItemInfo>(), TransTime = r.trantime };
                    orderDict.Add(r.khddid, o);
                }
                o.Items.Add(new OrderItemInfo() { c_cpdn_ecode = r.pinming, c_cpdn_ename = r.xinghao, Price = r.FPrice, Qty = r.FQty, Freight = r.Freight, Status = r.Status });
            }
            //summary totalAmount
            foreach (OrderInfo ord in orderDict.Values)
            {
                ord.Amount = ord.Items.Sum(e => e.Amount);
            }
            return orderDict.Values.ToList();
        }
    }
}