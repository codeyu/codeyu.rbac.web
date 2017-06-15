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

namespace Rbac.Web.Areas.SysManage.Controllers
{
    /// <summary>
    /// 操作权限
    /// </summary>
    public class BasePermissionItemController : BaseController
    {

        BasePermissionItemBll basePermissionItemBll = new BasePermissionItemBll();
        AjaxMsg ajaxMsg = new AjaxMsg();


        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [MurphyActionFilter]
        public ActionResult Index()
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            var permissionItemList = basePermissionItemBll.GetListWhere(null, null);
            ViewBag.TotalItems = permissionItemList.Count;
            return View(permissionItemList);
        }

        /// <summary>
        /// 创建操作权限项
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 创建操作权限项
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string Create(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BasePermissionItem basePermissionItem = new BasePermissionItem();
                basePermissionItem.Id = Guid.NewGuid().ToString();
                basePermissionItem.ParentId = frmCol["ParentId"].IsNullOrEmpty() ? "0" : frmCol["ParentId"];
                basePermissionItem.Name = frmCol["Name"].Trim();
                basePermissionItem.Code = frmCol["Code"].Trim();
                basePermissionItem.Description = frmCol["Description"].Trim();
                basePermissionItem.SortCode = frmCol["SortCode"].Trim();
                basePermissionItem.Enabled = Convert.ToInt32(frmCol["Enabled"]);
                basePermissionItem.AllowUpdate = Convert.ToInt32(frmCol["AllowUpdate"]);
                basePermissionItem.AllowDelete = Convert.ToInt32(frmCol["AllowDelete"]);
                basePermissionItem.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                basePermissionItem.CreateUserId = RequestCache.GetCacheUser().UserId;
                if (basePermissionItemBll.Create(basePermissionItem))
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
        /// 编辑操作权限项
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(basePermissionItemBll.GetEntity(id));
        }

        /// <summary>
        /// 编辑操作权限项
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string Update(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BasePermissionItem basePermissionItem = basePermissionItemBll.GetEntity(frmCol["ItemId"]);
                basePermissionItem.ParentId = frmCol["ParentId"].IsNullOrEmpty() ? "0" : frmCol["ParentId"];
                basePermissionItem.Name = frmCol["Name"].Trim();
                basePermissionItem.Code = frmCol["Code"].Trim();
                basePermissionItem.Description = frmCol["Description"].Trim();
                basePermissionItem.SortCode = frmCol["SortCode"].Trim();
                basePermissionItem.Enabled = Convert.ToInt32(frmCol["Enabled"]);
                basePermissionItem.AllowUpdate = Convert.ToInt32(frmCol["AllowUpdate"]);
                basePermissionItem.AllowDelete = Convert.ToInt32(frmCol["AllowDelete"]);
                basePermissionItem.ModifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                basePermissionItem.ModifyUserId = RequestCache.GetCacheUser().UserId;

                if (basePermissionItemBll.Update(basePermissionItem) > 0)
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
        /// 删除操作权限项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string Delete(string id)
        {
            try
            {
                if (basePermissionItemBll.Delete(id) > 0)
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
        /// 检测操作权限项名是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpPost]
        public string CheckPermissionItemName(string name, string itemId)
        {
            try
            {
                int count = basePermissionItemBll.GetListWhere(" Name='" + name.Trim() + "' AND Id!='" + itemId + "'", null).ToList().Count;
                if (count > 0)
                {
                    ajaxMsg.Msg = "操作权限项名称已存在";
                    ajaxMsg.Status = "+ERROR";
                }
                else
                {
                    ajaxMsg.Msg = "操作权限项名称输入正确";
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

        /// <summary>
        /// 检测操作权限项编码是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpPost]
        public string CheckPermissionItemCode(string code, string itemId)
        {
            try
            {
                int count = basePermissionItemBll.GetListWhere(" Code='" + code.Trim() + "' AND Id!='" + itemId + "'", null).ToList().Count;
                if (count > 0)
                {
                    ajaxMsg.Msg = "操作权限项编码已存在";
                    ajaxMsg.Status = "+ERROR";
                }
                else
                {
                    ajaxMsg.Msg = "操作权限项编码输入正确";
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
