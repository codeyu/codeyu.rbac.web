using Rbac.Business;
using Rbac.Core;
using Rbac.Entity;
using Rbac.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rbac.Web.Areas.Generator.Controllers
{
    /// <summary>
    /// 代码生成
    /// </summary>
    public class DbServerController : BaseController
    {

        DataBaseBll dataBaseBll = new DataBaseBll();
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
        /// 数据表树形
        /// </summary>
        /// <returns></returns>
        public ActionResult TableTree()
        {
            return View();
        }

        /// <summary>
        /// 得到数据表树形
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetTableTree()
        {
            return dataBaseBll.GetTableTree();
        }

        /// <summary>
        /// 表字段列表
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public ActionResult FieldList(string dataBaseName, string tableName)
        {
            string pager = string.Empty;
            string query = string.Format(@"&dataBaseName={0}&tableName={1}", dataBaseName, tableName);
            var fieldList = dataBaseBll.GetPageFieldListWhere(out pager, dataBaseName, tableName, query, null);
            ViewBag.Pager = pager;
            ViewBag.TotalItems = fieldList.TotalItems;
            return View(fieldList.Items);
        }


        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public ActionResult CreateCode(string dataBaseName, string tableName)
        {
            ViewBag.DataBaseName = dataBaseName;
            ViewBag.TableName = tableName;
            return View();
        }


        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public string CreateCode(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                //保存目录
                string directoryPath = new ConfigDirectory().GetDefault().Name;
                if (!string.IsNullOrEmpty(directoryPath))
                {
                    string dataBaseName = frmCol["DataBaseName"].Trim();
                    string tableName = frmCol["TableName"].Trim();
                    //需要生成那些曾
                    string projectValue = frmCol["ProjectValue"].Trim();
                    //生成的文件名
                    string fileName = string.Empty;
                    //得到不重复的列表
                    Hashtable project = HashtableHelper.StringKeyValueToHashtable(projectValue);
                    DataBaseParam dataBaseParam = new DataBaseParam();
                    dataBaseParam.Company = frmCol["Company"].Trim();
                    dataBaseParam.CreateYear = frmCol["CreateYear"].Trim();
                    dataBaseParam.Author = frmCol["Author"].Trim();
                    dataBaseParam.CreateDate = frmCol["CreateDate"].Trim();
                    dataBaseParam.ClassName = tableName;

                    DataBaseNameSpaceClass cns = new ConfigNameSpaceClass().GetDefault();
                    StreamWriter sw;
                    //生成实体层
                    if (project["Entity"].ToString() == "checked")
                    {
                        dataBaseParam.NameSpaceClass = cns.Entity;
                        fileName = PublicMethod.ExistsDirectory(string.Format("{0}\\{1}\\{2}.cs", directoryPath, cns.Entity, tableName));
                        sw = System.IO.File.CreateText(fileName);
                        sw.Write(dataBaseBll.GetEntityClass(dataBaseName, tableName, dataBaseParam));
                        sw.Close();
                        sw.Dispose();
                    }
                    //生成业务逻辑层
                    if (project["Business"].ToString() == "checked")
                    {
                        dataBaseParam.NameSpaceClass = cns.Business;
                        fileName = PublicMethod.ExistsDirectory(string.Format("{0}\\{1}\\{2}.cs", directoryPath, cns.Business, tableName + "Bll"));
                        sw = System.IO.File.CreateText(fileName);
                        sw.Write(dataBaseBll.GetBusinessClass(dataBaseName, tableName, dataBaseParam));
                        sw.Close();
                        sw.Dispose();
                    }
                    //生成数据访问层
                    if (project["DataAccess"].ToString() == "checked")
                    {
                        dataBaseParam.NameSpaceClass = cns.Dal;
                        fileName = PublicMethod.ExistsDirectory(string.Format("{0}\\{1}\\{2}.cs", directoryPath, cns.Dal, tableName + "Dal"));
                        sw = System.IO.File.CreateText(fileName);
                        sw.Write(dataBaseBll.GetDalClass(dataBaseName, tableName, dataBaseParam));
                        sw.Close();
                        sw.Dispose();
                    }
                    //生成数据访问接口层
                    if (project["IDataAccess"].ToString() == "checked")
                    {
                        dataBaseParam.NameSpaceClass = cns.IDal;
                        fileName = PublicMethod.ExistsDirectory(string.Format("{0}\\{1}\\{2}.cs", directoryPath, cns.IDal, "I" + tableName + "Dal"));
                        sw = System.IO.File.CreateText(fileName);
                        sw.Write(dataBaseBll.GetIDalClass(dataBaseName, tableName, dataBaseParam));
                        sw.Close();
                        sw.Dispose();
                    }
                    ajaxMsg.Msg = "生成成功";
                    ajaxMsg.Status = "+OK";
                }
                else
                {
                    ajaxMsg.Msg = "保存目录不能为空";
                    ajaxMsg.Status = "+ERROR";
                }
             
            }
            catch (Exception ex)
            {
                ajaxMsg.Msg = ex.Message;
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }
    }
}
