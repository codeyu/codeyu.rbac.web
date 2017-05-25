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
    /// 组织机构
    /// </summary>
    public class BaseOrganizeController : BaseController
    {
        BaseOrganizeBll baseOrganizeBll = new BaseOrganizeBll();
        AjaxMsg ajaxMsg = new AjaxMsg();

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [MurphyActionFilter]
        public ActionResult Index()
        {
            ViewBag.TotalItems = baseOrganizeBll.GetListWhere(null, null).Count;
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 组织机构树
        /// </summary>
        /// <returns></returns>
        public ActionResult OrganizeTree()
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 获取机构树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetOrganizeTree()
        {
            return baseOrganizeBll.GetOrganizeListWhere(null, null);
        }

        /// <summary>
        /// 创建机构
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 创建机构
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string Create(FormCollection collection)
        {
            NameValueCollection frmCol = HttpContext.Request.Form;
            try
            {

                BaseOrganize baseOrganize = new BaseOrganize();
                baseOrganize.Id = Guid.NewGuid().ToString();
                baseOrganize.Name = frmCol["Name"].Trim();
                baseOrganize.FullName = frmCol["FullName"].Trim();
                baseOrganize.ParentId = frmCol["ParentId"].Trim();
                baseOrganize.Code = frmCol["Code"].Trim();
                baseOrganize.Type = Convert.ToInt32(frmCol["Type"]);
                baseOrganize.Office = frmCol["Office"].Trim();
                baseOrganize.Description = frmCol["Description"].Trim();
                baseOrganize.MobilePhone = frmCol["MobilePhone"].Trim();
                baseOrganize.Enabled = Convert.ToInt32(frmCol["Enabled"]);
                baseOrganize.SortCode = frmCol["SortCode"].IsNullOrEmpty() ? 0 : Convert.ToInt32(frmCol["SortCode"].Trim());
                baseOrganize.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseOrganize.CreateUserId = RequestCache.GetCacheUser().UserId;
                if (baseOrganizeBll.Create(baseOrganize))
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
        /// 编辑机构
        /// </summary>
        /// <returns></returns>
        [MurphyActionFilter]
        public ActionResult Update(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            BaseOrganize baseOrganize = null;
            if (!string.IsNullOrEmpty(id))
            {
                baseOrganize = baseOrganizeBll.GetEntity(id);

            }
            else
            {
                baseOrganize = new BaseOrganize();
            }
            return View(baseOrganize);

        }

        /// <summary>
        /// 编辑机构
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string Update(FormCollection collection)
        {
            NameValueCollection frmCol = HttpContext.Request.Form;
            try
            {
                BaseOrganize baseOrganize = baseOrganizeBll.GetEntity(frmCol["OrganizeId"]);
                baseOrganize.Name = frmCol["Name"].Trim();
                baseOrganize.FullName = frmCol["FullName"].Trim();
                baseOrganize.ParentId = frmCol["ParentId"].Trim();
                baseOrganize.Code = frmCol["Code"].Trim();
                baseOrganize.Type = Convert.ToInt32(frmCol["Type"]);
                baseOrganize.Office = frmCol["Office"].Trim();
                baseOrganize.Description = frmCol["Description"].Trim();
                baseOrganize.MobilePhone = frmCol["MobilePhone"].Trim();
                baseOrganize.Enabled = Convert.ToInt32(frmCol["Enabled"]);
                baseOrganize.SortCode = frmCol["SortCode"].IsNullOrEmpty() ? 0 : Convert.ToInt32(frmCol["SortCode"].Trim());
                baseOrganize.ModifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseOrganize.ModifyUserId = RequestCache.GetCacheUser().UserId;

                if (baseOrganizeBll.Update(baseOrganize) > 0)
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
        /// 检测机构名是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        [HttpPost]
        public string CheckOrganizeName(string name, string organizeId)
        {
            try
            {
                int count = baseOrganizeBll.GetListWhere(" Name='" + name.Trim() + "' AND Id!='" + organizeId + "'", null).ToList().Count;

                if (count > 0)
                {
                    ajaxMsg.Msg = "机构简称已存在";
                    ajaxMsg.Status = "+ERROR";
                }
                else
                {
                    ajaxMsg.Msg = "机构简称输入正确";
                    ajaxMsg.Status = "+OK";
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
        /// 分配角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public ActionResult AllotRole(string departmentId, string departmentName)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            ViewBag.DepartmentName = departmentName;
            ViewBag.DepartmentId = departmentId;
            return View(baseOrganizeBll.GetOrganizeRoleListWhere(departmentId));
        }

        /// <summary>
        /// 保存分配的角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>

        [HttpPost]
        public string SaveAllotRole(string departmentId, string roleIds)
        {
            try
            {
                if (baseOrganizeBll.SaveAllotRole(departmentId, roleIds))
                {
                    ajaxMsg.Msg = "分配角色成功";
                    ajaxMsg.Status = "+OK";
                }
                else
                {
                    ajaxMsg.Msg = "分配角色失败";
                    ajaxMsg.Status = "+ERROR";
                }
            }
            catch (Exception ex)
            {
                ajaxMsg.Msg = "分配角色失败,出现异常信息";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }
    }
}
