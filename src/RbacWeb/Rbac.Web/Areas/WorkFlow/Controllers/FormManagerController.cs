using Murphy.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Murphy.Web.Areas.WorkFlow.Controllers
{
    /// <summary>
    /// 表单管理
    /// </summary>
    public class FormManagerController : Controller
    {

        WorkFlow_BillBll billBll = new WorkFlow_BillBll();

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string billName = Request.Params["billName"];
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(billName))
            {
                where += " AND Name like'%" + billName + "%'";
            }
            string query = string.Format(@"&Name={0}", billName);
            string pager = string.Empty;
            var billList = billBll.GetPageListWhere(out pager, where, query, null);
            ViewBag.Pager = pager;
            ViewBag.BillName = billName;
            ViewBag.TotalItems = billList.TotalItems;
            return View(billList.Items);
        }

        /// <summary>
        /// 字段列表
        /// </summary>
        /// <param name="billId"></param>
        /// <returns></returns>
        public ActionResult FieldList(string billId)
        {
            ViewBag.BillId = billId;
            return View();
        }
        

        /// <summary>
        /// 字段编辑
        /// </summary>
        /// <param name="billId"></param>
        /// <returns></returns>
        public ActionResult FieldUpdate(string billId)
        {
            return View();
        }
    }
}
