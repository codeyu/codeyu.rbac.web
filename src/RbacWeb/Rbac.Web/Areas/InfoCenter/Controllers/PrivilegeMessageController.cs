using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rbac.Web.Areas.InfoCenter.Controllers
{
    public class PrivilegeMessageController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.TopModuleCode = "a79fb5fe-781f-44fb-91b3-0e70b5acb407";
            return View();
        }

    }
}
