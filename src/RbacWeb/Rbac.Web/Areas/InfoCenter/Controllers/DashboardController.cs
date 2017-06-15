using Rbac.Business;
using Rbac.Core;
using Rbac.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Rbac.Web.Areas.InfoCenter.Controllers
{
    /// <summary>
    /// 导航首页
    /// </summary>
    public class DashboardController : BaseController
    {

        BaseAccessBll baseAccessBll = new BaseAccessBll();

        /// <summary>
        /// 导航首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            List<BaseAccess> accessList = baseAccessBll.GetHighchartAccessLog(RequestCache.GetCacheUser().UserId);
            GregorianCalendar gc = new GregorianCalendar();
            StringBuilder xDate = new StringBuilder();
            StringBuilder yDateCount = new StringBuilder();
            int year = int.Parse(DateTime.Now.ToString("yyyy"));
            int month = int.Parse(DateTime.Now.ToString("MM"));
            int days = gc.GetDaysInMonth(year, month);
            xDate.Append("[");
            yDateCount.Append("[");
            for (int i = 1; i <= days; i++)
            {
                xDate.Append(i + ",");
                List<BaseAccess> strRes = accessList.Where(u => u.day == i).ToList();
                if (strRes.Count > 0)
                {

                    yDateCount.Append(strRes[0].count + ",");
                }
                else
                {
                    yDateCount.Append(0 + ",");
                }
            }
            xDate.Append("]");
            yDateCount.Append("]");
            ViewBag.text = month + "月份登陆次数";
            ViewBag.xDate = xDate;
            ViewBag.yDateCount = yDateCount;
            ViewBag.month = month;
            ViewBag.TopModuleCode = "a79fb5fe-781f-44fb-91b3-0e70b5acb407";
            return View();
        }

    }
}
