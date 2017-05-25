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
    /// 类库命名
    /// </summary>
    public class NameSpaceClassController : BaseController
    {

        ConfigNameSpaceClass cns = new ConfigNameSpaceClass();
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
        /// 保存类库命名
        /// </summary>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        [HttpPost]
        public string SaveNameSpaceClass(string namespaces)
        {
            try
            {
                //先删除所有的
                List<DataBaseNameSpaceClass> list = cns.GetAll();
                foreach (var item in list)
                {
                    cns.Delete(item.Entity);
                }

                var namespacesList = namespaces.Split(new string[1] { "≌" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var namespacesItem in namespacesList)
                {
                    if (!string.IsNullOrEmpty(namespacesItem))
                    {
                        DataBaseNameSpaceClass dbns = new DataBaseNameSpaceClass();
                        dbns.Entity = namespacesItem.Split('☻')[0];
                        dbns.Business = namespacesItem.Split('☻')[1];
                        dbns.Dal = namespacesItem.Split('☻')[2];
                        dbns.IDal = namespacesItem.Split('☻')[3];
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
        /// 删除类库命名
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteNameSpaceClass(string entityName)
        {
            try
            {
                cns.Delete(entityName);
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
