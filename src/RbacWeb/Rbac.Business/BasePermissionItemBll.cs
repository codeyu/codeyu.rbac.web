using Murphy.Entity;
using Murphy.IData;
using Murphy.Core;
using Newtonsoft.Json;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murphy.Business
{
    public class BasePermissionItemBll : BaseBll<BasePermissionItem>
    {
          protected IBasePermissionItemDal basePermissionItemDal = null;
          public BasePermissionItemBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            basePermissionItemDal = springContext.GetObject("BasePermissionItemDal") as IBasePermissionItemDal;
            base.idal = basePermissionItemDal;
        }

          /// <summary>
          /// 获取操作权限项树  选择上级
          /// </summary>
          /// <param name="where"></param>
          /// <param name="orderBy"></param>
          /// <param name="itemId"></param>
          /// <returns></returns>
          public string GetPermissionItemListWhere(string where, string orderBy, string itemId)
          {
              List<BaseTree> moduleTreeList = new List<BaseTree>();
              ConvertPermissionItem(basePermissionItemDal.GetListWhere(null, null), moduleTreeList, "0", itemId);
              return JsonConvert.SerializeObject(moduleTreeList);
          }

          /// <summary>
          /// 递归转换操作权限项
          /// </summary>
          /// <param name="permissionItemList"></param>
          /// <param name="permissionItemTreeList"></param>
          /// <param name="parentId"></param>
          /// <param name="itemId"></param>
          public void ConvertPermissionItem(List<BasePermissionItem> permissionItemList, List<BaseTree> permissionItemTreeList, string parentId, string itemId)
          {
              foreach (var permissionItem in permissionItemList.Where(o => o.ParentId == parentId))
              {

                  BaseTree permissionItemTree = new BaseTree()
                  {
                      id = permissionItem.Id,
                      pid = permissionItem.ParentId,
                      name = permissionItem.Name,
                      pname = permissionItem.ParentName,
                      font = new font() { color = "#08c" },
                      code = permissionItem.Code,
                      Amount = permissionItem.Amount,
                      isleaf = permissionItem.isleaf,
                      PermissionItemSortCode = permissionItem.SortCode
                  };
                 

                  if (permissionItem.Id == itemId)
                  {
                      permissionItemTree.isChecked = true;
                  }
                  permissionItemTree.children = new List<BaseTree>();
                  permissionItemTreeList.Add(permissionItemTree);
                  ConvertPermissionItem(permissionItemList, permissionItemTree.children, permissionItemTree.id, itemId);
              }
          }
    }
}
