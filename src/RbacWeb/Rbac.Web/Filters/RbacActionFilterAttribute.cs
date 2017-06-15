using HtmlAgilityPack;
using Rbac.Entity;
using Rbac.Utils;
using Rbac.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Rbac.Web.Filters
{
    public class MurphyActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            //用户控件则不进行筛选
            if (!filterContext.IsChildAction)
                filterContext.HttpContext.Response.Filter = new WhitespaceFilter(filterContext.HttpContext.Response, filterContext);//重写
        }
    }
    //重写
    public class WhitespaceFilter : System.IO.MemoryStream
    {
        private System.IO.Stream Filter = null;
        private ResultExecutedContext filterContext = null;
        private string Source = string.Empty;

        //构造函数，用来接收变量
        public WhitespaceFilter(HttpResponseBase HttpResponseBase, ResultExecutedContext filterContexts)
        {
            Filter = HttpResponseBase.Filter;
            filterContext = filterContexts;
        }

        //分析进行权限处理
        public override void Close()
        {

            //要验证的区域名 集合
            List<string> listFieldLimit = new List<string>() { "/sysmanage/baseuser/userlist"};
            //获取区域名
            string strArea = filterContext.RouteData.DataTokens.Keys.Contains("area") ? filterContext.RouteData.DataTokens["area"].ToString().ToLower() : "";
            //获取控制器名
            string strController = filterContext.RouteData.Values.Keys.Contains("controller") ? filterContext.RouteData.Values["controller"].ToString().ToLower() : "";
            //获取动作名
            string strAction = filterContext.RouteData.Values.Keys.Contains("action") ? filterContext.RouteData.Values["action"].ToString().ToLower() : "";

            string navigationUrl = string.Format(@"/{0}/{1}/{2}", strArea, strController, strAction);
           
            var model = filterContext.Controller.ViewData.Model;
            var view = filterContext.RouteData.Values["action"].ToString().ToLower();
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(filterContext.Controller.ControllerContext, view);
                //filterContext.Controller.ViewData.Model = model;
                var viewContext = new ViewContext(filterContext,
                                          viewResult.View,
                                          filterContext.Controller.ViewData,
                                          filterContext.Controller.TempData,
                                          sw);
                try
                {
                    viewResult.View.Render(viewContext, sw);
                }
                catch (Exception ex)
                {
                    LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
                }
                Source = sw.GetStringBuilder().ToString();
            }
            //解析处理
            HtmlDocument Document = new HtmlDocument();
            Document.LoadHtml(Source);
            HtmlNode htmlNode = Document.DocumentNode;

            /*这里需要获取这个模块下所有的共功能权限，然后跟你所拥有的这个页面的功能权限比对，如果不拥有这个功能权限，则可以根据规则获取到这段HTML，然后删除掉*/
            /*_______________________开始分析处理功能按钮，这里可以自己增加验证规则___________________________*/
            HtmlNodeCollection hnc = htmlNode.SelectNodes("//a");//获取需要验证的功能按钮HTML，由开发人员自己定义，你也可以给个特定的标识来标识这个标签为功能按钮，譬如：htmlNode.SelectNodes("//a[@class='add']");获取神马的
            HtmlNodeCollection buttonHnc = htmlNode.SelectNodes("//button");
            HtmlNodeCollection thHnc = htmlNode.SelectNodes("//th");
            HtmlNodeCollection tdHnc = htmlNode.SelectNodes("//td");
            HtmlNodeCollection divHnc = htmlNode.SelectNodes("//div");
            if (hnc != null)
            {

                //获取操作按钮权限
                var baseButtonPermissionList = RequestCache.GetCacheSystem().ButtonPermissionObj as List<BaseButtonPermission>;
                if (baseButtonPermissionList != null && baseButtonPermissionList.Count > 0)
                {
                    List<string> baseButtonCodeList = baseButtonPermissionList.Select(u => u.ButtonCode).ToList();
                    if (hnc != null)
                    {
                        foreach (HtmlNode node in hnc)
                        {
                            //拿到所有A标签，然后把href取出来，跟当前用户所拥有的功能权限比对，如果相等或者包含则删除
                            string CodeStr = node.Attributes["murphybuttonname"] != null ? node.Attributes["murphybuttonname"].Value: "";
                            //node.ParentNode.RemoveAll();
                            if (!string.IsNullOrEmpty(CodeStr))
                            {
                                if (!baseButtonCodeList.Contains(CodeStr))
                                {
                                    node.Remove();
                                }
                            }
                        }
                    }
                    if (buttonHnc != null)
                    {
                        foreach (HtmlNode node in buttonHnc)
                        {
                            //拿到所有A标签，然后把href取出来，跟当前用户所拥有的功能权限比对，如果相等或者包含则删除
                            string CodeStr = node.Attributes["murphybuttonname"] != null ? node.Attributes["murphybuttonname"].Value: "";
                            //node.ParentNode.RemoveAll();
                            if (!string.IsNullOrEmpty(CodeStr))
                            {
                                if (!baseButtonCodeList.Contains(CodeStr))
                                {
                                    node.Remove();
                                }
                            }
                        }
                    }
                }
            }

            if (listFieldLimit.Contains(navigationUrl))
            {
                //获取字段操作权限（需要屏蔽的字段）
                List<BaseFieldPermission> baseFieldPermissionList = RequestCache.GetCacheSystem().FieldPermissionObj as List<BaseFieldPermission>;
                if (baseFieldPermissionList != null && baseFieldPermissionList.Count > 0)
                {
                    List<string> baseFieldCodeList = baseFieldPermissionList.Select(u => u.FieldCode).ToList();
                    //过滤字段
                    if (thHnc != null)
                    {
                        foreach (HtmlNode node in thHnc)
                        {
                            string CodeStr = node.Attributes["murphyfieldname"] != null ? node.Attributes["murphyfieldname"].Value: "";
                            if (!string.IsNullOrEmpty(CodeStr))
                            {
                                //if (!baseFieldCodeList.Contains(CodeStr))
                                //{
                                //    node.Remove();
                                //}
                                if (baseFieldCodeList.Contains(CodeStr))
                                {
                                    node.Remove();
                                }
                            }
                        }
                    }
                    //过滤字段
                    if (tdHnc != null)
                    {
                        foreach (HtmlNode node in tdHnc)
                        {
                            string CodeStr = node.Attributes["murphyfieldname"] != null ? node.Attributes["murphyfieldname"].Value: "";
                            if (!string.IsNullOrEmpty(CodeStr))
                            {
                                //if (!baseFieldCodeList.Contains(CodeStr))
                                //{
                                //    node.Remove();
                                //}
                                if (baseFieldCodeList.Contains(CodeStr))
                                {
                                    node.Remove();
                                }
                            }
                        }
                    }
                    //过滤字段
                    if (divHnc != null)
                    {
                        foreach (HtmlNode node in divHnc)
                        {
                            string CodeStr = node.Attributes["murphyfieldname"] != null ? node.Attributes["murphyfieldname"].Value : "";
                            if (!string.IsNullOrEmpty(CodeStr))
                            {
                                //if (!baseFieldCodeList.Contains(CodeStr))
                                //{
                                //    node.Remove();
                                //}
                                if (baseFieldCodeList.Contains(CodeStr))
                                {
                                    node.Remove();
                                }
                            }
                        }
                    }
                }
            }

            Source = htmlNode.InnerHtml;
            Filter.Write(System.Text.Encoding.UTF8.GetBytes(Source), 0, System.Text.Encoding.UTF8.GetByteCount(Source));
            base.Close();
        }
    }
}