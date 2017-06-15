using Rbac.Core;
using Rbac.Entity;
using Rbac.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rbac.Web.Areas.Generator.Controllers
{
    /// <summary>
    /// 保存目录
    /// </summary>
    public class DirectoryController : BaseController
    {

        ConfigDirectory cd = new ConfigDirectory();
        AjaxMsg ajaxMsg = new AjaxMsg();

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var list = cd.GetAll();
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View(list);
        }

        /// <summary>
        /// 保存目录
        /// </summary>
        /// <param name="directorys"></param>
        /// <returns></returns>
        [HttpPost]
        public string SaveDirectory(string directorys)
        {
            try
            {
                //先删除所有的
                List<DataBaseDirectory> list = cd.GetAll();
                foreach (var item in list)
                {
                    cd.Delete(item.Name);
                }
                var directoryList = directorys.Split(new string[1] { "☻" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var directory in directoryList)
                {
                    if (!string.IsNullOrEmpty(directory))
                    {
                        DataBaseDirectory dbd = new DataBaseDirectory();
                        dbd.Name = directory;
                        cd.Add(dbd);
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
        /// 删除目录
        /// </summary>
        /// <param name="directoryName"></param>
        /// <returns></returns>
        [HttpPost]
        public string DeleteDirectory(string directoryName)
        {
            try
            {
                cd.Delete(directoryName);
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
