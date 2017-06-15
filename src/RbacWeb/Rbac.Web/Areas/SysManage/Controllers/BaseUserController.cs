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
    /// 用户管理
    /// </summary>
    public class BaseUserController : BaseController
    {
        BaseUserBll baseUserBll = new BaseUserBll();
        BaseJobTitleBll baseJobTitleBll = new BaseJobTitleBll();
        BaseRoleBll baseRoleBll = new BaseRoleBll();
        BaseOrganizeBll baseOrganizeBll = new BaseOrganizeBll();
        AjaxMsg ajaxMsg = new AjaxMsg();

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 组织机构树
        /// </summary>
        /// <returns></returns>
        public ActionResult UserOrganizeTree()
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 获取组织机构树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetUserOrganizeTree()
        {
            return baseUserBll.GetUserOrganizeListWhere(null, null);
        }

        [MurphyActionFilter]
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList(string departmentId)
        {
            string departmentName = Request.Params["departmentName"];
            string roleName = Request.Params["roleName"];
            string userName = Request.Params["userName"];
            string where = " 1=1 AND IsSuper!=1 ";
            if (!string.IsNullOrEmpty(departmentId))
            {
                where += " AND DepartmentId='" + departmentId + "'";
            }
            if (!string.IsNullOrEmpty(departmentName))
            {
                where += " AND DepartmentName like'%" + departmentName + "%'";
            }
            if (!string.IsNullOrEmpty(roleName))
            {
                where += " AND RoleName like'%" + roleName + "%'";
            }
            if (!string.IsNullOrEmpty(userName))
            {
                where += " AND UserName like'%" + userName + "%'";
            }
            string pager = string.Empty;
            string query = string.Format(@"&DepartmentName={0}&RoleName={1}&UserName={2}", departmentName, roleName, userName);
            var userList = baseUserBll.GetPageListWhere(out pager, where,query, "departmentId");
            ViewBag.Pager = pager;
            ViewBag.UserName = userName;
            ViewBag.DepartmentName = departmentName;
            ViewBag.RoleName = roleName;
            ViewBag.TotalItems = userList.TotalItems;
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(userList.Items);
        }

        /// <summary>
        /// 用户岗位列表  选择上级用户
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public ActionResult UserJobTilteList(string departmentId)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public ActionResult CreateUser(string departmentId)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            BaseOrganize baseOrganize = baseOrganizeBll.GetEntity(departmentId);
            ViewBag.DepartmentId = baseOrganize.Id;
            ViewBag.DepartmentName = baseOrganize.Name;
            return View();
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string CreateUser(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BaseUser baseUser = new BaseUser();
                baseUser.Id = Guid.NewGuid().ToString();
                baseUser.Code = frmCol["WorkCode"].Trim();
                baseUser.UserName = frmCol["UserName"].Trim();
                baseUser.UserPassword = HashEncrypt.MD5System(frmCol["UserPassword"].Trim().ToString());
                baseUser.RealName = frmCol["RealName"].Trim();
                baseUser.Gender = frmCol["Gender"].IsNullOrEmpty() ? 0 : Convert.ToInt32(frmCol["Gender"]);
                baseUser.DepartmentId = frmCol["departmentId"];
                baseUser.CompanyId = baseUserBll.GetUserCompanyId(frmCol["departmentId"]).Id;
                baseUser.SubCompanyId = baseUserBll.GetUserSubCompanyId(frmCol["departmentId"]).Id;
                baseUser.JobTitle = frmCol["JobTitle"];
                baseUser.ManagerId = frmCol["managerId"];
                baseUser.RoleId = frmCol["RoleId"];
                baseUser.Enabled = frmCol["Status"].IsNullOrEmpty() ? 0 : Convert.ToInt32(frmCol["Status"]); ;
                baseUser.MobilePhone = frmCol["MobilePhone"].Trim();
                baseUser.Email = frmCol["Email"].Trim();
                baseUser.SecLevel = frmCol["SecurityLevel"].IsNullOrEmpty() ? 0 : Convert.ToInt32(frmCol["SecurityLevel"]);
                baseUser.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseUser.CreateUserId = RequestCache.GetCacheUser().UserId;

                if (baseUserBll.Create(baseUser))
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
        /// 编辑用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateUser(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(baseUserBll.GetEntity(id));
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateUser(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BaseUser baseUser = baseUserBll.GetEntity(frmCol["UserId"]);
                baseUser.Code = frmCol["WorkCode"].Trim();
                baseUser.UserName = frmCol["UserName"].Trim();
                baseUser.RealName = frmCol["RealName"].Trim();
                baseUser.Gender = frmCol["Gender"].IsNullOrEmpty() ? 0 : Convert.ToInt32(frmCol["Gender"]);
                baseUser.DepartmentId = frmCol["departmentId"];
                baseUser.CompanyId = baseUserBll.GetUserCompanyId(frmCol["departmentId"]).Id;
                baseUser.SubCompanyId = baseUserBll.GetUserSubCompanyId(frmCol["departmentId"]).Id;
                baseUser.JobTitle = frmCol["JobTitle"];
                baseUser.ManagerId = frmCol["managerId"];
                baseUser.RoleId = frmCol["RoleId"];
                baseUser.Enabled = frmCol["Status"].IsNullOrEmpty() ? 0 : Convert.ToInt32(frmCol["Status"]); ;
                baseUser.MobilePhone = frmCol["MobilePhone"].Trim();
                baseUser.Email = frmCol["Email"].Trim();
                baseUser.SecLevel = frmCol["SecurityLevel"].IsNullOrEmpty() ? 0 : Convert.ToInt32(frmCol["SecurityLevel"]);
                baseUser.ModifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseUser.ModifyUserId = RequestCache.GetCacheUser().UserId;

                if (baseUserBll.Update(baseUser) > 0)
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
        /// 改变用户状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string ChangeUserStatus(int status, string id)
        {
            try
            {
                BaseUser baseUser = baseUserBll.GetEntity(id);
                baseUser.Enabled = status;

                if (baseUserBll.Update(baseUser) > 0)
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
        /// 用户授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserPermission(string id)
        {
            var baseUser = baseUserBll.GetEntity(id);
            ViewBag.RealName = baseUser.RealName;
            ViewBag.DepartmentName = baseUser.DepartmentName;
            ViewBag.UserId = baseUser.Id;
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 用户对应的模块权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserModulePermission(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            ViewBag.UserId = id;
            return View();
        }

        /// <summary>
        /// 获取用户对应的模块权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetUserModulePermission(string id)
        {
            return baseUserBll.GetUserModuleListByUserId(id);
        }


        /// <summary>
        /// 保存用户对应对应的模块权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>

        [HttpPost]
        public string UpdateUserModulePermission(string id, string accessValueArray)
        {
            try
            {
                if (baseUserBll.UpdateUserModuleList(id, accessValueArray))
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
        /// 用户对应的操作按钮权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserButtonPermission(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            ViewBag.UserId = id;
            return View();
        }

        /// <summary>
        /// 获取用户对应的操作按钮权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetUserButtonPermission(string id)
        {
            return baseUserBll.GetUserButtonListByUserId(id);
        }

        /// <summary>
        /// 保存用户对应对应的操作按钮权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>

        [HttpPost]
        public string UpdateUserButtonPermission(string id, string accessValueArray)
        {
            try
            {
                if (baseUserBll.UpdateUserButtonList(id, accessValueArray))
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
        /// 用户对应的字段权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserFieldPermission(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            ViewBag.UserId = id;
            return View();
        }

        /// <summary>
        /// 获取用户对应的字段权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetUserFieldPermission(string id)
        {
          
            return baseUserBll.GetUserFieldListByUserId(id);
        }

        /// <summary>
        /// 保存用户对应对应的字段权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>

        [HttpPost]
        public string UpdateUserFieldPermission(string id, string accessValueArray)
        {
            try
            {
                if (baseUserBll.UpdateUserFieldList(id, accessValueArray))
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
        /// 用户对应的数据权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserDataPermission(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            var constraint = baseUserBll.GetUserDataPermissionConstraint(id);
            ViewBag.UserId = id;
            ViewBag.Constraint = constraint;
            return View();
        }

        /// <summary>
        /// 获取用户对应的数据权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetUserDataPermission(string id)
        {
            return baseUserBll.GetUserDataDetailsListByUserId(id);
        }

        // <summary>
        /// 保存用户对应对应的数据权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accessValueArray"></param>
        /// <returns></returns>

        [HttpPost]
        public string UpdateUserDataPermission(string id, string accessValueArray, string constraint)
        {
            //只要不是勾选明细单选框 就删除掉组织机构选中的节点值
            if (constraint != "Details")
            {
                accessValueArray = string.Empty;
            }
            try
            {
                if (baseUserBll.UpdateUserDataList(id, accessValueArray, constraint))
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
        /// 获取指定部门下的岗位
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetJobTitleListByDepartmentId(string departmentId)
        {
            return JsonConvert.SerializeObject(baseJobTitleBll.GetListWhere(" DepartmentId='" + departmentId + "' and Enabled=1 ", null));
        }

        /// <summary>
        /// 获取指定部门下的角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetRoleListByDepartmentId(string departmentId)
        {
            return JsonConvert.SerializeObject(baseRoleBll.GetRoleListByDepartmentId(departmentId));
        }

        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public string CheckUserName(string userName, string userId)
        {
            int count = baseUserBll.GetListWhere(" UserName='" + userName.Trim() + "' AND Id!='" + userId + "'", null).ToList().Count;
            if (count > 0)
            {
                ajaxMsg.Msg = "登陆名已存在";
                ajaxMsg.Status = "+ERROR";
            }
            else
            {
                ajaxMsg.Msg = "登陆名输入正确";
                ajaxMsg.Status = "+OK";
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }


        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="realName"></param>
        /// <returns></returns>
        public ActionResult AllotRole(string userId, string realName)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            ViewBag.RealName = realName;
            ViewBag.UserId = userId;
            return View(baseUserBll.GetUserRoleListWhere(userId));
        }

        /// <summary>
        /// 保存分配的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>

        [HttpPost]
        public string SaveAllotRole(string userId, string roleIds)
        {
            try
            {
                if (baseUserBll.SaveAllotRole(userId, roleIds))
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
