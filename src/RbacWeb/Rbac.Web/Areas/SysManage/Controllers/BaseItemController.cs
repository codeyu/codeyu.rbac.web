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
    /// 数据字典
    /// </summary>
    public class BaseItemController : BaseController
    {
        BaseItemBll baseItemBll = new BaseItemBll();
        BaseItemDetailBll baseItemDetailBll = new BaseItemDetailBll();
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
        /// 字典类别树
        /// </summary>
        /// <returns></returns>
        public ActionResult ItemTree()
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 得到字典类别树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetItemTree()
        {

            return baseItemBll.GetItemTree();
        }

        /// <summary>
        /// 创建字典类别
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateItem()
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            return View();
        }

        /// <summary>
        /// 得到条目列表
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public string GetItemList()
        {
            List<BaseItem> baseItemList = baseItemBll.GetListWhere(null, null);
            return JsonConvert.SerializeObject(baseItemList);
        }

        /// <summary>
        /// 保存字典类别
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        [HttpPost]
        public string SaveItem(string items)
        {
            try
            {
                //新参数值   要插入的值
                var listNewPar = new List<BaseItem>();
                var itemList = items.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var item in itemList)
                {
                    BaseItem baseItem = new BaseItem();
                    baseItem.Id = item.Split('|')[4].IsNullOrEmpty() ? Guid.NewGuid().ToString() : item.Split('|')[4];
                    baseItem.Code = item.Split('|')[0];
                    baseItem.ParentId = "0";
                    baseItem.Enabled = 1;
                    baseItem.Name = item.Split('|')[1];
                    baseItem.Description = item.Split('|')[2];
                    baseItem.SortCode = Convert.ToInt32(item.Split('|')[3]);
                    listNewPar.Add(baseItem);
                }



                //原来的参数值  要删除的数据
                var listOldPar = baseItemBll.GetListWhere(null, null).ToList();
                var listNewParId = listNewPar.Select(u => u.Id).ToList();
                //要更新的数据
                var lisyUpdatePar = new List<BaseItem>();
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


                if (baseItemBll.SaveItem(listNewPar, lisyUpdatePar, listOldPar))
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
                ajaxMsg.Msg = "保存失败";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }

        /// <summary>
        /// 字典明细
        /// </summary>
        /// <returns></returns>
       [MurphyActionFilter]
        public ActionResult ItemDetailList(string itemId)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(itemId))
            {
                where += " AND ItemId='" + itemId + "'";
            }
            string pager = string.Empty;
            string query = string.Empty;
            var roleList = baseItemDetailBll.GetPageListWhere(out pager, where, query, null);
            ViewBag.Pager = pager;
            ViewBag.TotalItems = roleList.TotalItems;
            return View(roleList.Items);
        }

        /// <summary>
        /// 创建字典明细
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ActionResult CreateItemDetail(string itemId)
       {
           ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            ViewBag.ItemId = itemId;
            return View();
        }

        /// <summary>
        /// 创建字典明细
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string CreateItemDetail(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BaseItemDetail baseItemDetail = new BaseItemDetail();
                baseItemDetail.Id = Guid.NewGuid().ToString();
                baseItemDetail.ItemId = frmCol["ItemId"].Trim();
                baseItemDetail.SelectValue = frmCol["SelectValue"].Trim();
                baseItemDetail.SelectText = frmCol["SelectText"].Trim();
                baseItemDetail.Description = frmCol["Description"].Trim();
                baseItemDetail.Enabled = string.IsNullOrEmpty(frmCol["Enabled"]) ? 0 : Convert.ToInt32(frmCol["Enabled"]);
                baseItemDetail.IsDefault = string.IsNullOrEmpty(frmCol["IsDefault"]) ? 0 : Convert.ToInt32(frmCol["IsDefault"]);
                baseItemDetail.SortCode = string.IsNullOrEmpty(frmCol["SortCode"]) ? 0 : Convert.ToInt32(frmCol["SortCode"]);
                baseItemDetail.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseItemDetail.CreateUserId = RequestCache.GetCacheUser().UserId;

                if (baseItemDetailBll.Create(baseItemDetail))
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
                ajaxMsg.Msg = "保存失败";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }

        /// <summary>
        /// 编辑字典明细
        /// </summary>
        /// <param name="itemDetailId"></param>
        /// <returns></returns>
        public ActionResult UpdateItemDetail(string itemDetailId)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            BaseItemDetail baseItemDetail = baseItemDetailBll.GetEntity(itemDetailId);
            return View(baseItemDetail);
        }

        /// <summary>
        /// 编辑字典明细
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string UpdateItemDetail(FormCollection collection)
        {
            try
            {
                NameValueCollection frmCol = HttpContext.Request.Form;
                BaseItemDetail baseItemDetail = baseItemDetailBll.GetEntity(frmCol["itemDetailId"]);
                baseItemDetail.SelectValue = frmCol["SelectValue"].Trim();
                baseItemDetail.SelectText = frmCol["SelectText"].Trim();
                baseItemDetail.Description = frmCol["Description"].Trim();
                baseItemDetail.Enabled = string.IsNullOrEmpty(frmCol["Enabled"]) ? 0 : Convert.ToInt32(frmCol["Enabled"]);
                baseItemDetail.IsDefault = string.IsNullOrEmpty(frmCol["IsDefault"]) ? 0 : Convert.ToInt32(frmCol["IsDefault"]);
                baseItemDetail.SortCode = string.IsNullOrEmpty(frmCol["SortCode"]) ? 0 : Convert.ToInt32(frmCol["SortCode"]);
                baseItemDetail.ModifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                baseItemDetail.ModifyUserId = RequestCache.GetCacheUser().UserId;

                if (baseItemDetailBll.Update(baseItemDetail) > 0)
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
                ajaxMsg.Msg = "保存失败";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }

        /// <summary>
        /// 删除字典明细
        /// </summary>
        /// <param name="itemDetailId"></param>
        /// <returns></returns>
        public string DeleteItemDetail(string itemDetailId)
        {
            ViewBag.TopModuleCode = "2EA6D6E0-06AC-4BF6-A53F-48C0D7FC304D";
            try
            {
                if (baseItemDetailBll.Delete(itemDetailId) > 0)
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
                ajaxMsg.Msg = "删除失败";
                ajaxMsg.Status = "+ERROR";
                LogHelper.ErrorLog(string.Format(@"{0}里出现异常信息", RequestHelper.GetScriptName), ex);
            }
            return JsonConvert.SerializeObject(ajaxMsg);
        }
    }
}
