using Murphy.Business;
using Murphy.Entity;
using Murphy.Utils;
using Murphy.Core;
using Murphy.Web.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Murphy.Web.Areas.SysManage.Controllers
{
    /// <summary>
    /// 功能模块
    /// </summary>
    public class BaseModuleController : BaseController
    {
        BaseModuleBll baseModuleBll = new BaseModuleBll();
        AjaxMsg ajaxMsg = new AjaxMsg();
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [MurphyActionFilter]
        public ActionResult Index()
        {
            var moduleList = baseModuleBll.GetListWhere(null, null);
            ViewBag.TotalItems = moduleList.Count;
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(moduleList);
        }


        /// <summary>
        /// 创建模块
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 创建模块
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string Create(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BaseModule baseModule = new BaseModule();
                baseModule.Id = Guid.NewGuid().ToString();
                baseModule.ParentId = frmCol["ParentId"].IsNullOrEmpty() ? "0" : frmCol["ParentId"];
                baseModule.Name = frmCol["Name"].Trim();
                baseModule.Code = frmCol["Code"].Trim();
                baseModule.IconUrl = frmCol["IconUrl"].Trim();
                baseModule.NavigationTarget = frmCol["NavigationTarget"].Trim();
                baseModule.NavigationUrl = frmCol["NavigationUrl"].Trim();
                baseModule.SortCode = frmCol["SortCode"].Trim();
                baseModule.IsVisible = Convert.ToInt32(frmCol["IsVisible"]);
                baseModule.Enabled = Convert.ToInt32(frmCol["Enabled"]);
                baseModule.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseModule.CreateUserId = RequestCache.GetCacheUser().UserId;

                if (baseModuleBll.Create(baseModule))
                {
                    ajaxMsg.Msg = "保存成功";
                    ajaxMsg.Status = "+OK";
                }
                else
                {
                    ajaxMsg.Msg = "保存失败";
                    ajaxMsg.Status = "+ERROR";
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
        /// 编辑模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(baseModuleBll.GetEntity(id));
        }

        /// <summary>
        /// 编辑模块
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string Update(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BaseModule baseModule = baseModuleBll.GetEntity(frmCol["ModuleId"]);
                baseModule.ParentId = frmCol["ParentId"].IsNullOrEmpty() ? "0" : frmCol["ParentId"];
                baseModule.Name = frmCol["Name"].Trim();
                baseModule.Code = frmCol["Code"].Trim();
                baseModule.IconUrl = frmCol["IconUrl"].Trim();
                baseModule.NavigationTarget = frmCol["NavigationTarget"].Trim();
                baseModule.NavigationUrl = frmCol["NavigationUrl"].Trim();
                baseModule.SortCode = frmCol["SortCode"].Trim();
                baseModule.IsVisible = Convert.ToInt32(frmCol["IsVisible"]);
                baseModule.Enabled = Convert.ToInt32(frmCol["Enabled"]);
                baseModule.ModifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseModule.ModifyUserId = RequestCache.GetCacheUser().UserId;

                if (baseModuleBll.Update(baseModule) > 0)
                {
                    ajaxMsg.Msg = "保存成功";
                    ajaxMsg.Status = "+OK";
                }
                else
                {
                    ajaxMsg.Msg = "保存失败";
                    ajaxMsg.Status = "+ERROR";
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
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string Delete(string id)
        {
            try
            {
                if (baseModuleBll.Delete(id) > 0)
                {
                    ajaxMsg.Msg = "删除成功";
                    ajaxMsg.Status = "+OK";
                }
                else
                {
                    ajaxMsg.Msg = "删除失败";
                    ajaxMsg.Status = "+ERROR";
                }
            }
            catch (Exception ex)
            {
                ajaxMsg.Msg = "删除失败,出现异常信息";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }

        /// <summary>
        /// 检测模块名是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [HttpPost]
        public string CheckModuleName(string name, string moduleId)
        {
            try
            {
                int count = baseModuleBll.GetListWhere(" Name='" + name.Trim() + "' AND Id!='" + moduleId + "'", null).ToList().Count;
                if (count > 0)
                {
                    ajaxMsg.Msg = "模块名称已存在";
                    ajaxMsg.Status = "+ERROR";
                }
                else
                {
                    ajaxMsg.Msg = "模块名称输入正确";
                    ajaxMsg.Status = "+OK";
                }
            }
            catch (Exception ex)
            {
                ajaxMsg.Msg = "出现异常信息";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }
    }
}
