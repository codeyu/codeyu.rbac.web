using Murphy.Core;
using Murphy.Entity;
using Murphy.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Murphy.Web.Areas.Generator.Controllers
{
    /// <summary>
    /// 命名空间
    /// </summary>
    public class NamespaceController : BaseController
    {

        ConfigNameSpace cns = new ConfigNameSpace();
        AjaxMsg ajaxMsg = new AjaxMsg();

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var list = cns.GetAll();
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(list);
        }

        /// <summary>
        /// 保存命名空间
        /// </summary>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        [HttpPost]
        public string SaveNameSpace(string namespaces)
        {
            try
            {
                //先删除所有的
                List<DataBaseNameSpace> list = cns.GetAll();
                foreach (var item in list)
                {
                    cns.Delete(item.Name1);
                }
                var namespacesList = namespaces.Split(new string[1] { "≌" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var namespacesItem in namespacesList)
                {
                    if (!string.IsNullOrEmpty(namespacesItem))
                    {
                        DataBaseNameSpace dbns = new DataBaseNameSpace();
                        dbns.Name1 = namespacesItem.Split('☻')[0];
                        dbns.Name2 = namespacesItem.Split('☻')[1];
                        cns.Add(dbns);
                    }
                }
            }
            catch (Exception ex)
            {
                ajaxMsg.Msg = "保存失败,出现异常信息";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            ajaxMsg.Msg = "保存成功";
            ajaxMsg.Status = "+OK";
            return JsonConvert.SerializeObject(ajaxMsg);
        }

        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="name1Space"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteNameSpace(string name1Space)
        {
            try
            {
                cns.Delete(name1Space);
            }
            catch (Exception ex)
            {
                ajaxMsg.Msg = "保存失败,出现异常信息";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            ajaxMsg.Msg = "删除成功";
            ajaxMsg.Status = "+OK";
            return JsonConvert.SerializeObject(ajaxMsg);
        }
    }
}
