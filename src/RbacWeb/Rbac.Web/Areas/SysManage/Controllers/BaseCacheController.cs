using Rbac.Business;
using Rbac.Entity;
using Rbac.Utils;
using Rbac.Core;
using Rbac.Web.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rbac.Web.Areas.SysManage.Controllers
{
    /// <summary>
    /// 缓存设置
    /// </summary>
    public class BaseCacheController : BaseController
    {
        BaseStorageBll baseStorageBll = new BaseStorageBll();
        BaseUserBll baseUserBll = new BaseUserBll();
        AjaxMsg ajaxMsg = new AjaxMsg();

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [MurphyActionFilter]
        public ActionResult Index()
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }


        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        public string Update(string userIdList, string cacheKey)
        {
            switch (cacheKey)
            {
                case "UserInfo":
                    try
                    {
                        foreach (var userId in userIdList.Split(','))
                        {
                            UpdateCacheUser(userId);
                        }
                    }
                    catch (Exception ex)
                    {
                        ajaxMsg.Msg = "更新失败,出现异常信息";
                        ajaxMsg.Status = "+ERROR";
                        LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
                    }
                    ajaxMsg.Msg = "更新成功";
                    ajaxMsg.Status = "+OK";
                    break;
                case "DataPermission":
                    try
                    {
                        foreach (var userId in userIdList.Split(','))
                        {
                            string DataPermissionKey = userId + "_DataPermission";
                            UpdateCacheUser(userId);
                            HttpContext.Cache[DataPermissionKey] = baseStorageBll.GetDataPermission(RequestCache.GetCacheUser());
                        }
                    }
                    catch (Exception ex)
                    {
                        ajaxMsg.Msg = "更新失败,出现异常信息";
                        ajaxMsg.Status = "+ERROR";
                        LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
                    }
                    ajaxMsg.Msg = "更新成功";
                    ajaxMsg.Status = "+OK";
                    break;
                case "FieldPermission":
                    try
                    {
                        foreach (var userId in userIdList.Split(','))
                        {
                            string FieldPermissionKey = userId + "_FieldPermission";
                            HttpContext.Cache[FieldPermissionKey] = baseStorageBll.GetFieldPermission(userId);
                        }
                    }
                    catch (Exception ex)
                    {
                        ajaxMsg.Msg = "更新失败,出现异常信息";
                        ajaxMsg.Status = "+ERROR";
                        LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
                    }
                    ajaxMsg.Msg = "更新成功";
                    ajaxMsg.Status = "+OK";
                    break;
                case "ButtonPermission":
                    try
                    {
                        foreach (var userId in userIdList.Split(','))
                        {
                            string ButtonPermissionKey = userId + "_ButtonPermission";
                            HttpContext.Cache[ButtonPermissionKey] = baseStorageBll.GetButtonPermission(userId);
                        }
                    }
                    catch (Exception ex)
                    {
                        ajaxMsg.Msg = "更新失败,出现异常信息";
                        ajaxMsg.Status = "+ERROR";
                        LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
                    }
                    ajaxMsg.Msg = "更新成功";
                    ajaxMsg.Status = "+OK";
                    break;
                case "ModulePermission":
                    try
                    {
                        foreach (var userId in userIdList.Split(','))
                        {
                            string ModulePermissionKey = userId + "_ModulePermission";
                            HttpContext.Cache[ModulePermissionKey] = baseStorageBll.GetModulePermission(userId);
                        }
                    }
                    catch (Exception ex)
                    {
                        ajaxMsg.Msg = "更新失败,出现异常信息";
                        ajaxMsg.Status = "+ERROR";
                        LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
                    }
                    ajaxMsg.Msg = "更新成功";
                    ajaxMsg.Status = "+OK";
                    break;
                default:
                    break;
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }

        /// <summary>
        /// 更新用户缓存信息
        /// </summary>
        /// <param name="userId"></param>
        public void UpdateCacheUser(string userId)
        {
            var baseUser = baseUserBll.GetEntity(userId);
            CacheUser cacheUser = new CacheUser();
            cacheUser.UserId = baseUser.Id;
            cacheUser.Code = baseUser.Code;
            cacheUser.UserName = baseUser.UserName;
            cacheUser.UserPassword = baseUser.UserPassword;
            cacheUser.RealName = baseUser.RealName;
            cacheUser.Gender = baseUser.Gender;
            cacheUser.RoleId = baseUser.RoleId;
            cacheUser.JobTitle = baseUser.JobTitle;
            cacheUser.CompanyId = baseUser.CompanyId;
            cacheUser.SubCompanyId = baseUser.SubCompanyId;
            cacheUser.DepartmentId = baseUser.DepartmentId;
            cacheUser.WorkGroupId = baseUser.WorkGroupId;
            cacheUser.RoleName = baseUser.RoleName;
            cacheUser.JobTitleName = baseUser.JobTitleName;
            cacheUser.CompanyName = baseUser.CompanyName;
            cacheUser.SubCompanyName = baseUser.SubCompanyName;
            cacheUser.DepartmentName = baseUser.DepartmentName;
            cacheUser.WorkGroupName = baseUser.WorkGroupName;
            RequestCache.AddCacheUser(cacheUser);
        }
    }
}
