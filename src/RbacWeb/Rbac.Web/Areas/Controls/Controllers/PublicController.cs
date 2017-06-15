using Rbac.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rbac.Web.Areas.Controls.Controllers
{
    public class PublicController : Controller
    {
        BaseUserBll baseUserBll = new BaseUserBll();
        BaseRoleBll baseRoleBll = new BaseRoleBll();
        BaseOrganizeBll baseOrganizeBll = new BaseOrganizeBll();
        BaseModuleBll baseModuleBll = new BaseModuleBll();
        BasePermissionItemBll basePermissionItemBll = new BasePermissionItemBll();

        /// <summary>
        /// 选中操作权限项
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPermissionItem()
        {
            return View();
        }

        /// <summary>
        /// 获取操作权限项树
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetPermissionItemTree(string itemId)
        {
            return basePermissionItemBll.GetPermissionItemListWhere(null, null, itemId);
        }

        /// <summary>
        /// 选择组织机构树
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectOrganize()
        {
            return View();
        }
      

        /// <summary>
        /// 获取组织机构树
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetOrganizeTree(string departmentId)
        {
            return baseOrganizeBll.GetOrganizeListWhere(null, null, departmentId);
        }

        /// <summary>
        /// 选择用户
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectHrmResource()
        {
            string userName = Request.Params["userName"];
            string where = " 1=1 AND IsSuper!=1 ";
            if (!string.IsNullOrEmpty(userName))
            {
                where += " AND UserName like'%" + userName + "%'";
            }
            string pager = string.Empty;
            string query = string.Format(@"UserName={0}",userName);
            var userList = baseUserBll.GetPageListWhere(out pager, where, query,"departmentId");
            ViewBag.Pager = pager;
            ViewBag.UserName = userName;
            ViewBag.TotalItems = userList.TotalItems;
            return View(userList.Items);
        }

        /// <summary>
        /// 选择角色
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectRole()
        {
            string where = " 1=1 AND Enabled==1 AND Code!=super";
            string pager = string.Empty;
            string query = string.Empty;
            var roleList = baseRoleBll.GetPageListWhere(out pager, where, query,null);
            ViewBag.Pager = pager;
            ViewBag.TotalItems = roleList.TotalItems;
            return View(roleList.Items);
        }

        /// <summary>
        /// 选择模块
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectModule()
        {
            return View();
        }

      /// <summary>
        /// 获取功能模块树
      /// </summary>
      /// <param name="moduleId"></param>
      /// <returns></returns>
        [HttpPost]
        public string GetModuleTree(string moduleId)
        {
            return baseModuleBll.GetModuleListWhere(null, null, moduleId);
        }

        
    }
}
