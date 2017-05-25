using Murphy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Murphy.Web.Areas
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {

            var areaName = filterContext.RouteData.DataTokens.Keys.Contains("area") ? filterContext.RouteData.DataTokens["area"].ToString() : "";
            var controllerName = filterContext.RouteData.Values.Keys.Contains("controller") ? filterContext.RouteData.Values["controller"].ToString() : "";
            var actionName = filterContext.RouteData.Values.Keys.Contains("action") ? filterContext.RouteData.Values["action"].ToString() : "";
            filterContext.Controller.ViewData["ErrorMessage"] = filterContext.Exception.Message;
            filterContext.Controller.ViewData["StackTrace"] = filterContext.Exception.StackTrace;
            filterContext.Controller.ViewData["areaName"] = areaName;
            filterContext.Controller.ViewData["controllerName"] = controllerName;
            filterContext.Controller.ViewData["actionName"] = actionName;

            LogHelper.ErrorLog(string.Format(@"区域名:{0},控制器名:{1},方法名:{2}里出现异常信息", areaName, controllerName, actionName), filterContext.Exception);

            // 跳转到错误页
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = filterContext.Controller.ViewData
            };
            // 标记异常已处理
            filterContext.ExceptionHandled = true;
        }

    }
}
