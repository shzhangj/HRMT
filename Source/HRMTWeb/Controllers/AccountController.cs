using HRMTWeb.Action;
using HRMTWeb.Models;
using OppenIT.Core.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMTWeb.Controllers
{
    [Authentication]
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MemberView()
        {
            //TODO::客户ID
            CustomerInfo customer = null;
            try
            {
                using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
                {
                    customer = dbc.QueryFirst<CustomerInfo>(@"SELECT
c_wldw.wldwid AS CustomerID,
c_wldw.ecode AS CustomerCode,
c_wldw.ename AS CustomerName,
s_employee.ename AS ContactPerson,
s_employee.lxfs AS ContactTel,
c_wldw.khwsk AS UnPayAmount,
c_wldw.dfhje AS UnfilledOrderAmount
FROM c_wldw
LEFT JOIN s_employee on (c_wldw.employeeid = s_employee.employeeid) 
LEFT  JOIN c_wlxxdn on (c_wldw.wlxxdnid = c_wlxxdn.wlxxdnid) WHERE wldwid=@CustomerID", new { CustomerID = AppSessionHelper.User.wldwid });
                }
            }
            catch (Exception ex)
            {
                ViewData["RetMessage"] = ex.Message;
            }
            return View("MemberView", customer);
        }
    }
}
