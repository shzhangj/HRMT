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
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult DoLogin(string form_username, string form_password)
        {
            string errText = "";
            UserInfo user = null;
            try
            {
                if (string.IsNullOrEmpty(form_username))
                    throw new Exception("请输入账号!");
                if (string.IsNullOrEmpty(form_password))
                    throw new Exception("请输入密码!");
                string md5Pwd = Utils.MD5(form_password);
                using (EntityDBContext dbc = EntityInitializer.GetDBContext(AppConstants.DbPair.Main))
                {
                    user = dbc.QueryFirst<UserInfo>("SELECT wldwid,ecode,ename,FPwd FROM c_wldw WHERE ecode=@ecode", new { ecode = form_username });
                }
                if (user == null)
                    throw new Exception("账号或密码错误!");
                if (user.FPwd != form_password && user.FPwd != md5Pwd)
                    throw new Exception("账号或密码错误!");
            }
            catch (Exception ex)
            {
                errText = ex.Message;
            }
            if (errText != "")
            {
                ViewData["RetMessage"] = errText;
                ViewData["UserCode"] = form_username;
                return View("Login");
            }
            else
            {
                AppSessionHelper.User = user;
                return RedirectToAction("MemberView", "Account");
            }
        }
        [Authentication]
        [HttpPost]
        public ActionResult ChangePwd(string newPWD)
        {
            string errText = "";
            try
            {
                UserAction.UpdatePassword(AppSessionHelper.User.wldwid, newPWD);
            }
            catch (Exception ex)
            {
                errText = ex.Message;
            }
            var retJObj = new { IsSuccess = string.IsNullOrEmpty(errText), ErrText = errText };
            return Json(retJObj);
        }
    }
}
