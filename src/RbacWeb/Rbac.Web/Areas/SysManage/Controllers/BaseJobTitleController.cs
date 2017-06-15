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
    /// 岗位管理
    /// </summary>
    public class BaseJobTitleController : BaseController
    {

        BaseJobTitleBll baseJobTitleBll = new BaseJobTitleBll();
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
        /// 岗位组织机构树
        /// </summary>
        /// <returns></returns>
        public ActionResult JobTitleOrganizeTree()
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 获取岗位组织机构树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetJobTitleOrganizeTree()
        {
            return baseJobTitleBll.GetJobTitleOrganizeTree();
        }

        /// <summary>
        /// 岗位列表
        /// </summary>
        /// <returns></returns>
        [MurphyActionFilter]
        public ActionResult JobTitleList(string departmentId)
        {
            string jobTitleName = Request.Params["jobTitleName"];
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(departmentId))
            {
                where += " AND DepartmentId='" + departmentId + "'";
            }
            if (!string.IsNullOrEmpty(jobTitleName))
            {
                where += " AND Name like'%" + jobTitleName + "%'";
            }
            string pager = string.Empty;
            string query = string.Format(@"&Name={0}&DepartmentId={1}", jobTitleName, departmentId);
            var roleList = baseJobTitleBll.GetPageListWhere(out pager, where, query, null);
            ViewBag.Pager = pager;
            ViewBag.JobTitleName = jobTitleName;
            ViewBag.TotalItems = roleList.TotalItems;
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(roleList.Items);
        }

        /// <summary>
        /// 创建岗位
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public ActionResult Create(string departmentId)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            ViewBag.DepartmentId = departmentId;
            return View();
        }

        /// <summary>
        /// 创建岗位
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string Create(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BaseJobTitle baseJobTitle = new BaseJobTitle();
                baseJobTitle.Id = Guid.NewGuid().ToString();
                baseJobTitle.DepartmentId = frmCol["departmentId"];
                baseJobTitle.Name = frmCol["Name"].Trim();
                baseJobTitle.Code = frmCol["Code"].Trim();
                baseJobTitle.Description = frmCol["Description"].Trim();
                baseJobTitle.Enabled = Convert.ToInt32(frmCol["Enabled"]);
                baseJobTitle.SortCode = frmCol["SortCode"].IsNullOrEmpty() ? 0 : Convert.ToInt32(frmCol["SortCode"].Trim());
                baseJobTitle.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseJobTitle.CreateUserId = RequestCache.GetCacheUser().UserId;

                if (baseJobTitleBll.Create(baseJobTitle))
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
        /// 编辑岗位
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(string id)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(baseJobTitleBll.GetEntity(id));
        }

        /// <summary>
        /// 编辑岗位
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string Update(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BaseJobTitle baseJobTitle = baseJobTitleBll.GetEntity(frmCol["JobTitleId"]);
                baseJobTitle.Name = frmCol["Name"].Trim();
                baseJobTitle.Code = frmCol["Code"].Trim();
                baseJobTitle.Description = frmCol["Description"].Trim();
                baseJobTitle.Enabled = Convert.ToInt32(frmCol["Enabled"]);
                baseJobTitle.SortCode = frmCol["SortCode"].IsNullOrEmpty() ? 0 : Convert.ToInt32(frmCol["SortCode"].Trim());
                baseJobTitle.ModifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseJobTitle.ModifyUserId = RequestCache.GetCacheUser().UserId;

                if (baseJobTitleBll.Update(baseJobTitle) > 0)
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
        /// 删除岗位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Delete(string id)
        {
            try
            {
                if (baseJobTitleBll.Delete(id) > 0)
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
        /// 检测岗位名称是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="jobTitleId"></param>
        /// <returns></returns>
        [HttpPost]
        public string CheckJobTitleName(string name, string jobTitleId)
        {
            int count = baseJobTitleBll.GetListWhere(" Name='" + name.Trim() + "' AND Id!='" + jobTitleId + "'", null).ToList().Count;
            if (count > 0)
            {
                ajaxMsg.Msg = "岗位名称已存在";
                ajaxMsg.Status = "+ERROR";
            }
            else
            {
                ajaxMsg.Msg = "岗位名称输入正确";
                ajaxMsg.Status = "+OK";
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }
    }
}
