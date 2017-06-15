using Rbac.Business;
using Rbac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rbac.Web.Areas.SysManage.Controllers
{
    public class BaseLogController : BaseController
    {

        BaseLogBll baseLogBll = new BaseLogBll();

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string date = Request.Params["date"];
            string RealName = Request.Params["RealName"];
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(date))
            {
                where += " AND CONVERT(varchar(10),Date, 120 )='" + date + "'";
            }
            if (!string.IsNullOrEmpty(RealName))
            {
                where += " AND RealName LIKE '%" + RealName + "%'";
            }
            string pager = string.Empty;
            string query = string.Format(@"&date={0}&RealName={1}", date, RealName);
            var baseLogList = baseLogBll.GetPageListWhere(out pager, where,query, null);
            ViewBag.Pager = pager;
            ViewBag.Date = date;
            ViewBag.RealName = RealName;
            ViewBag.TotalItems = baseLogList.TotalItems;
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(baseLogList.Items);
        }

        /// <summary>
        /// 操作日志明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(baseLogBll.GetEntity(id));
        }
    }
}
