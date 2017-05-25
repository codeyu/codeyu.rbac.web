using Murphy.Entity;
using Murphy.IData;
using Murphy.Utils;
using Murphy.Core;
using Newtonsoft.Json;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Murphy.Business
{
    public class BaseItemBll : BaseBll<BaseItem>
    {
         protected IBaseItemDal dal = null;


         public BaseItemBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            dal = springContext.GetObject("BaseItemDal") as IBaseItemDal;
            base.idal = dal;
        }

         /// <summary>
         /// 字典类别树
         /// </summary>
         /// <returns></returns>
         public string GetItemTree()
         {
             List<BaseTree> itemTreeList = new List<BaseTree>();
             List<BaseTree> itemRootTreeList = new List<BaseTree>();
             GetItemTree(dal.GetListWhere(null, null), itemTreeList, "0");
             BaseTree rootTree = new BaseTree();
             rootTree.id = Guid.NewGuid().ToString();
             rootTree.name = "所有类别";
             rootTree.font = new font() { color = "#08c" };
             rootTree.children = itemTreeList;
             itemRootTreeList.Add(rootTree);
             return JsonConvert.SerializeObject(rootTree);
         }

         /// <summary>
         /// 递归转换字典类别树
         /// </summary>
         /// <param name="itemList"></param>
         /// <param name="itemTreeList"></param>
         /// <param name="parentId"></param>
         public void GetItemTree(List<BaseItem> itemList, List<BaseTree> itemTreeList, string parentId)
         {
             foreach (var item in itemList.Where(o => o.ParentId == parentId))
             {
                 BaseTree baseTree = new BaseTree()
                 {
                     id = item.Id,
                     pid = item.ParentId,
                     url = "/SysManage/BaseItem/ItemDetailList?itemId=" + item.Id,
                     target = "rightFrame",
                     font = new font() { color = "#08c" },
                     code = item.Code,
                     SortCode = item.SortCode
                 };
                 if (item.Enabled != 1)
                 {
                     baseTree.name = "<span style='text-decoration: line-through;'>" + item.Name + "</span>";
                 }
                 else
                 {
                     baseTree.name = item.Name;
                 }
                 baseTree.children = new List<BaseTree>();
                 itemTreeList.Add(baseTree);
                 GetItemTree(itemList, baseTree.children, baseTree.id);
             }
         }

         /// <summary>
         /// 保存字典类别
         /// </summary>
         /// <param name="items"></param>
         public bool SaveItem(string items)
         {
             string[] itemsList = items.Split(',');
             foreach (var item in itemsList)
             {
                 if (!string.IsNullOrEmpty(item))
                 {

                     int count = GetListWhere(" Id='" + item.Split('|')[4] + "'", null).Count;
                     if (count > 0)
                     {
                         BaseItem baseItem = base.GetListWhere(" Id='" + item.Split('|')[4] + "'", null).SingleOrDefault();
                         baseItem.Code = item.Split('|')[0];
                         baseItem.Name = item.Split('|')[1];
                         baseItem.Description = item.Split('|')[2];
                         baseItem.SortCode = Convert.ToInt32(item.Split('|')[3]);
                         baseItem.ModifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                         baseItem.ModifyUserId = RequestCache.GetCacheUser().UserId;
                         //编辑
                         //Update(baseItem);
                     }
                     else
                     {
                         BaseItem baseItem = new BaseItem();
                         baseItem.Id = Guid.NewGuid().ToString();
                         baseItem.Code = item.Split('|')[0];
                         baseItem.Name = item.Split('|')[1];
                         baseItem.Description = item.Split('|')[2];
                         baseItem.ParentId = "0";
                         baseItem.Enabled = 1;
                         baseItem.SortCode = Convert.ToInt32(item.Split('|')[3]);
                         baseItem.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                         baseItem.CreateUserId = RequestCache.GetCacheUser().UserId;
                         //创建
                        // Create(baseItem);
                     }
                 }
             }
             return true;
         }

         /// <summary>
         /// 保存字典类别
         /// </summary>
         /// <param name="insertParameters"></param>
         /// <param name="updateParameters"></param>
         /// <param name="deleteParameters"></param>
         /// <returns></returns>
         public bool SaveItem(List<BaseItem> insertItems, List<BaseItem> updateItems, List<BaseItem> deleteItems)
         {
             using (TransactionScope tsScope = new TransactionScope())
             {
                 //插入
                 foreach (var insertItem in insertItems)
                 {
                     if (insertItem != null)
                     {
                         Create(insertItem);
                     }
                 }
                 //更新
                 foreach (var updateItem in updateItems)
                 {
                     if (updateItem != null)
                     {
                         Update(updateItem);
                     }
                 }
                 //删除
                 foreach (var deleteItem in deleteItems)
                 {
                     if (deleteItem != null)
                     {
                         Delete(deleteItem.Id);
                     }
                 }
                 tsScope.Complete();
             }
             return true;
         }
    }
}
