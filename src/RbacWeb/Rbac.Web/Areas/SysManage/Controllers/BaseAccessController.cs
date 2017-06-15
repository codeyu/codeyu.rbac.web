using Rbac.Business;
using Rbac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rbac.Web.Areas.SysManage.Controllers
{
    /// <summary>
    /// 登陆日志
    /// </summary>
    public class BaseAccessController : BaseController
    {
        BaseAccessBll baseAccessBll = new BaseAccessBll();

        public ActionResult Index()
        {
            string date = Request.Params["date"];
            string Username = Request.Params["Username"];
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(date))
            {
                where += " AND CONVERT(varchar(10),Date, 120 )='" + date + "'";
            }
            if (!string.IsNullOrEmpty(Username))
            {
                where += " AND RealName LIKE '%" + Username + "%'";
            }
            string query = string.Format(@"&date={0}&Username={1}",date,Username);
            string pager = string.Empty;
            var baseAccessList = baseAccessBll.GetPageListWhere(out pager, where,query,null);
            ViewBag.Pager = pager;
            ViewBag.Date = date;
            ViewBag.UserName = Username;
            ViewBag.TotalItems = baseAccessList.TotalItems;
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(baseAccessList.Items);
        }

    }
}
