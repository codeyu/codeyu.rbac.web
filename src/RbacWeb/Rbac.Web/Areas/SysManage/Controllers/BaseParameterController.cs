using Murphy.Business;
using Murphy.Entity;
using Murphy.Utils;
using Murphy.Core;
using Murphy.Web.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Murphy.Web.Areas.SysManage.Controllers
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public class BaseParameterController : BaseController
    {
        BaseParameterBll baseParameterBll = new BaseParameterBll();
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
        /// 得到条目列表
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public string GetParameterList()
        {
            List<BaseParameter> parameterList = baseParameterBll.GetListWhere(null, null);
            return JsonConvert.SerializeObject(parameterList);
        }

         /// <summary>
        /// 保存系统参数
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        [HttpPost]
        public string SaveParameter(string parameters)
        {
            try
            {
               
                //新参数值   要插入的值
                var listNewPar = new List<BaseParameter>();
                var parametersList = parameters.Split(new string[1] { "≌" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var parametersItem in parametersList)
                {
                    if (!string.IsNullOrEmpty(parametersItem))
                    {
                        BaseParameter baseParameter = new BaseParameter();
                        baseParameter.Id = parametersItem.Split('☻')[5].IsNullOrEmpty() ? Guid.NewGuid().ToString() : parametersItem.Split('☻')[5];
                        baseParameter.Code = parametersItem.Split('☻')[0];
                        baseParameter.Value = parametersItem.Split('☻')[1];
                        baseParameter.Description = parametersItem.Split('☻')[2];
                        baseParameter.AllowUpdate = Convert.ToInt32(parametersItem.Split('☻')[3]);
                        baseParameter.SortOrder = parametersItem.Split('☻')[4].IsNullOrEmpty() ? 0 : Convert.ToInt32(parametersItem.Split('☻')[4]);
                        listNewPar.Add(baseParameter);
                    }
                }

                //var parameterList = parameters.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                //foreach (var parameter in parameterList)
                //{
                //    BaseParameter baseParameter = new BaseParameter();
                //    baseParameter.Id = parameter.Split('|')[5].IsNullOrEmpty() ? Guid.NewGuid().ToString() : parameter.Split('|')[5];
                //    baseParameter.Code = parameter.Split('|')[0];
                //    baseParameter.Value = parameter.Split('|')[1];
                //    baseParameter.Description = parameter.Split('|')[2];
                //    baseParameter.AllowUpdate = Convert.ToInt32(parameter.Split('|')[3]);
                //    baseParameter.SortOrder = parameter.Split('|')[4].IsNullOrEmpty() ? 0 : Convert.ToInt32(parameter.Split('|')[4]);
                //    listNewPar.Add(baseParameter);
                //}

                ////原来的参数值  要删除的数据
                //var listOldParId = baseParameterBll.GetListWhere(null, null).Select(p => p.Id).ToList();

                ////要新增的数据
                //var listNewParId = listNewPar.Select(u => u.Id).ToList();

                ////要更新的数据
                //var lisyUpdateParId = new List<string>();

                //for (int i = listOldParId.Count - 1; i >= 0; i--)
                //{
                //    string old = listOldParId[i];
                //    if (listNewParId.Contains(old.ToString()))
                //    {
                //        listOldParId.Remove(old);
                //        listNewParId.Remove(old.ToString());
                //        lisyUpdateParId.Add(old);
                //    }
                //}


                //原来的参数值  要删除的数据
                var listOldPar = baseParameterBll.GetListWhere(null, null).ToList();
                var listNewParId = listNewPar.Select(u => u.Id).ToList();
                //要更新的数据
                var lisyUpdatePar = new List<BaseParameter>();
                for (int i = listOldPar.Count - 1; i >= 0; i--)
                {
                    string old = listOldPar[i].Id;
                    if (listNewParId.Contains(old))
                    {
                        //lisyUpdatePar.Add(listOldPar[i]);
                        lisyUpdatePar.Add(listNewPar.Where(u => u.Id == old).FirstOrDefault());
                        listOldPar.Remove(listOldPar[i]);
                        listNewParId.Remove(old.ToString());
                    }
                }

                //要新增的数据
                for (int i = listNewPar.Count - 1; i >= 0; i--)
                {
                    string id = listNewPar[i].Id;
                    if (!listNewParId.Contains(id))
                    {
                        listNewPar.Remove(listNewPar[i]);
                    }
                }

                if (baseParameterBll.SaveParameter(listNewPar, lisyUpdatePar, listOldPar))
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
    }
}
