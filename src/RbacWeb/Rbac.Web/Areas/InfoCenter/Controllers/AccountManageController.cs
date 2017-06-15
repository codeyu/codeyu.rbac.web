using Rbac.Business;
using Rbac.Entity;
using Rbac.Utils;
using Rbac.Core;
using Rbac.Web.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rbac.Web.Areas.InfoCenter.Controllers
{
    /// <summary>
    /// 账号管理
    /// </summary>
    public class AccountManageController : BaseController
    {
        BaseUserBll baseUserBll = new BaseUserBll();
        BaseAccessBll baseAccessBll = new BaseAccessBll();

        AjaxMsg ajaxMsg = new AjaxMsg();

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>
        [MurphyActionFilter]
        public ActionResult Index()
        {
            BaseUser baseUser = baseUserBll.GetEntity(RequestCache.GetCacheUser().UserId);
            ViewBag.TopModuleCode = "a79fb5fe-781f-44fb-91b3-0e70b5acb407";
            return View(baseUser);
        }

        /// <summary>
        /// 更新个人信息
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateProfile(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BaseUser baseUser = baseUserBll.GetEntity(RequestCache.GetCacheUser().UserId);
                baseUser.RealName = frmCol["RealName"].Trim();
                baseUser.Gender = Convert.ToInt32(frmCol["Gender"]);
                baseUser.MobilePhone = frmCol["MobilePhone"].Trim();
                baseUser.Email = frmCol["Email"].Trim();
                baseUser.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseUser.CreateUserId = RequestCache.GetCacheUser().UserId;
                if (baseUserBll.Update(baseUser) > 0)
                {
                    ajaxMsg.Msg = "更新成功";
                    ajaxMsg.Status = "+OK";
                }
                else
                {
                    ajaxMsg.Msg = "更新失败";
                    ajaxMsg.Status = "+ERROR";
                }
            }
            catch (Exception ex)
            {
                ajaxMsg.Msg = "更新失败,出现异常信息";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
       [MurphyActionFilter]
        public ActionResult UpdatePassword()
        {
            ViewBag.TopModuleCode = "a79fb5fe-781f-44fb-91b3-0e70b5acb407";
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdatePassword(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                string oldPassword = frmCol["oldpassword"].Trim();
                string newPassword = frmCol["newpassword"].Trim();
                BaseUser baseUser = baseUserBll.GetEntity(RequestCache.GetCacheUser().UserId);
                if (baseUser != null)
                {
                    if (HashEncrypt.MD5System(oldPassword.Trim()) == baseUser.UserPassword)
                    {
                        baseUser.UserPassword = HashEncrypt.MD5System(newPassword.Trim());
                        baseUserBll.Update(baseUser);
                        ajaxMsg.Msg = "密码修改成功";
                        ajaxMsg.Status = "+OK";
                    }
                    else
                    {
                        ajaxMsg.Msg = "旧密码不正确";
                        ajaxMsg.Status = "+ERROR";
                    }
                }
                else
                {
                    HttpContext.Response.Redirect("/Home/LoginUser");
                }
               
            }
            catch (Exception ex)
            {
                ajaxMsg.Msg = "保存失败,出现异常信息";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }

        /// <summary>
        /// 登陆日志
        /// </summary>
        /// <returns></returns>
        public ActionResult AccessLog()
        {

            string date = Request.Params["date"];
            string Username = Request.Params["Username"];
            string where = " 1=1  AND UserId='" + RequestCache.GetCacheUser().UserId + "'";
            if (!string.IsNullOrEmpty(date))
            {
                where += " AND CONVERT(varchar(10),Date, 120 )='" + date + "'";
            }
            if (!string.IsNullOrEmpty(Username))
            {
                where += " AND RealName LIKE '%" + Username + "%'";
            }
            string query = string.Format(@"&date={0}&Username={1}", date, Username);
            string pager = string.Empty;
            var baseAccessList = baseAccessBll.GetPageListWhere(out pager, where, query, null);
            ViewBag.Pager = pager;
            ViewBag.Date = date;
            ViewBag.UserName = Username;
            ViewBag.TotalItems = baseAccessList.TotalItems;
            ViewBag.TopModuleCode = "a79fb5fe-781f-44fb-91b3-0e70b5acb407";
            return View(baseAccessList.Items);
        }
    }
}
