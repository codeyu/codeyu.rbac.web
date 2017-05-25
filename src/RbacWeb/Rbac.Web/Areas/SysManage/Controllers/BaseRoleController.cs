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
    /// 角色管理
    /// </summary>
    public class BaseRoleController : BaseController
    {
        BaseRoleBll baseRoleBll = new BaseRoleBll();
        AjaxMsg ajaxMsg = new AjaxMsg();

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [MurphyActionFilter]
        public ActionResult Index()
        {
            string roleName = Request.Params["roleName"];
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(roleName))
            {
                where += " AND Name like'%" + roleName + "%'";
            }
            string query = string.Format(@"&Name={0}", roleName);
            string pager = string.Empty;
            var roleList = baseRoleBll.GetPageListWhere(out pager, where, query, null);
            ViewBag.Pager = pager;
            ViewBag.RoleName = roleName;
            ViewBag.TotalItems = roleList.TotalItems;
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(roleList.Items);
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string Create(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BaseRole baseRole = new BaseRole();
                baseRole.Id = Guid.NewGuid().ToString();
                baseRole.Name = frmCol["Name"].Trim();
                baseRole.Code = frmCol["Code"].Trim();
                baseRole.Description = frmCol["Description"].Trim();
                baseRole.SortCode = frmCol["SortCode"].IsNullOrEmpty() ? 0 : Convert.ToInt32(frmCol["SortCode"].Trim());
                baseRole.AllowUpdate = Convert.ToInt32(frmCol["AllowUpdate"]);
                baseRole.AllowDelete = Convert.ToInt32(frmCol["AllowDelete"]);
                baseRole.Enabled = Convert.ToInt32(frmCol["Enabled"]);
                baseRole.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseRole.CreateUserId = RequestCache.GetCacheUser().UserId;

                if (baseRoleBll.Create(baseRole))
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
        /// 编辑角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(baseRoleBll.GetEntity(id));
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string Update(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BaseRole baseRole = baseRoleBll.GetEntity(frmCol["RoleId"]);
                baseRole.Name = frmCol["Name"].Trim();
                baseRole.Code = frmCol["Code"].Trim();
                baseRole.Description = frmCol["Description"].Trim();
                baseRole.SortCode = frmCol["SortCode"].IsNullOrEmpty() ? 0 : Convert.ToInt32(frmCol["SortCode"].Trim());
                baseRole.AllowUpdate = Convert.ToInt32(frmCol["AllowUpdate"]);
                baseRole.AllowDelete = Convert.ToInt32(frmCol["AllowDelete"]);
                baseRole.Enabled = Convert.ToInt32(frmCol["Enabled"]);
                baseRole.ModifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseRole.ModifyUserId = RequestCache.GetCacheUser().UserId;

                if (baseRoleBll.Update(baseRole) > 0)
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
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string Delete(string id)
        {
            try
            {
                if (baseRoleBll.DeleteRole(id))
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
        /// 检测角色名是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public string CheckRoleName(string name, string roleId)
        {
            try
            {
                int count = baseRoleBll.GetListWhere(" Name='" + name.Trim() + "' AND Id!='" + roleId + "'", null).ToList().Count;
                if (count > 0)
                {
                    ajaxMsg.Msg = "角色名称已存在";
                    ajaxMsg.Status = "+ERROR";
                }
                else
                {
                    ajaxMsg.Msg = "角色名称输入正确";
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
        /// 编辑权限
        /// </summary>
        /// <returns></returns>
        public ActionResult RolePermission(string id)
        {
            BaseRole baseRole = baseRoleBll.GetEntity(id);
            ViewBag.RoleName = baseRole.Name;
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 角色对应的模块权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RoleModulePermission(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            ViewBag.RoleId = id;
            return View();
        }

        /// <summary>
        /// 获取角色对应的模块权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetRoleModulePermission(string id)
        {
            return baseRoleBll.GetRoleModuleListByRoleId(id);
        }

        /// <summary>
        /// 保存角色对应的模块权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>

        [HttpPost]
        public string UpdateRoleModulePermission(string id, string accessValueArray)
        {
            try
            {
                if (baseRoleBll.UpdateRoleModuleList(id, accessValueArray))
                {
                    ajaxMsg.Msg = "授权成功";
                    ajaxMsg.Status = "+OK";
                }
                else
                {
                    ajaxMsg.Msg = "授权失败";
                    ajaxMsg.Status = "+ERROR";
                }
            }
            catch (Exception ex)
            {
                ajaxMsg.Msg = "授权失败,出现异常信息";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }

        /// <summary>
        /// 角色对应的操作按钮权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RoleButtonPermission(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            ViewBag.RoleId = id;
            return View();
        }

        /// <summary>
        /// 获取角色对应的操作按钮权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetRoleButtonPermission(string id)
        {
            return baseRoleBll.GetRoleButtonListByRoleId(id);
        }

        /// <summary>
        /// 保存角色对应的操作按钮权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>

        [HttpPost]
        public string UpdateRoleButtonPermission(string id, string accessValueArray)
        {
            try
            {
                if (baseRoleBll.UpdateRoleButtonList(id, accessValueArray))
                {
                    ajaxMsg.Msg = "授权成功";
                    ajaxMsg.Status = "+OK";
                }
                else
                {
                    ajaxMsg.Msg = "授权失败";
                    ajaxMsg.Status = "+ERROR";
                }
            }
            catch (Exception ex)
            {
                ajaxMsg.Msg = "授权失败,出现异常信息";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }


        /// <summary>
        /// 角色对应的字段权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RoleFieldPermission(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            ViewBag.RoleId = id;
            return View();
        }

        /// <summary>
        /// 获取角色对应的字段权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetRoleFieldPermission(string id)
        {
            return baseRoleBll.GetRoleFieldListByRoleId(id);
        }

        /// <summary>
        /// 保存角色对应的字段权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>

        [HttpPost]
        public string UpdateRoleFieldPermission(string id, string accessValueArray)
        {
            try
            {
                if (baseRoleBll.UpdateRoleFieldList(id, accessValueArray))
                {
                    ajaxMsg.Msg = "授权成功";
                    ajaxMsg.Status = "+OK";
                }
                else
                {
                    ajaxMsg.Msg = "授权失败";
                    ajaxMsg.Status = "+ERROR";
                }
            }
            catch (Exception ex)
            {
                ajaxMsg.Msg = "授权失败,出现异常信息";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }

        /// <summary>
        /// 角色对应的数据权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RoleDataPermission(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            var constraint = baseRoleBll.GetRoleDataPermissionConstraint(id);
            ViewBag.RoleId = id;
            ViewBag.Constraint = constraint;
            return View();
        }

        /// <summary>
        /// 获取用户对应的数据权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetRoleDataPermission(string id)
        {
            return baseRoleBll.GetRoleDataDetailsListByRoleId(id);
        }

        // <summary>
        /// 保存用户对应对应的数据权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>

        [HttpPost]
        public string UpdateRoleDataPermission(string id, string accessValueArray, string constraint)
        {
            //只要不是勾选明细单选框 就删除掉组织机构选中的节点值
            if (constraint != "Details")
            {
                accessValueArray = string.Empty;
            }
            try
            {
                if (baseRoleBll.UpdateRoleDataList(id, accessValueArray, constraint))
                {
                    ajaxMsg.Msg = "授权成功";
                    ajaxMsg.Status = "+OK";
                }
                else
                {
                    ajaxMsg.Msg = "授权失败";
                    ajaxMsg.Status = "+ERROR";
                }
            }
            catch (Exception ex)
            {
                ajaxMsg.Msg = "授权失败,出现异常信息";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }

    }
}
