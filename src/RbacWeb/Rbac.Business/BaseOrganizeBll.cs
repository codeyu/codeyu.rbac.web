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
    public class BaseOrganizeBll : BaseBll<BaseOrganize>
    {
       protected IBaseOrganizeDal baseOrganizeDal = null;
       public BaseOrganizeBll()
        {
            IApplicationContext springContext = ContextRegistry.GetContext();
            baseOrganizeDal = springContext.GetObject("BaseOrganizeDal") as IBaseOrganizeDal;
            base.idal = baseOrganizeDal;
        }


       /// <summary>
       /// 获取组织机构树  通用选择组织机构时需要
       /// </summary>
       /// <param name="where"></param>
       /// <param name="orderBy"></param>
       /// <returns></returns>
       public string GetOrganizeListWhere(string where, string orderBy, string departmentId)
       {
           List<BaseTree> organizeTreeList = new List<BaseTree>();
           ConvertOrganize(baseOrganizeDal.GetListWhere(null, null), organizeTreeList, "0", departmentId);
           return JsonConvert.SerializeObject(organizeTreeList);
       }



       /// <summary>
       /// 递归转换组织机构
       /// </summary>
       /// <param name="organizeList"></param>
       /// <param name="organizeTreeList"></param>
       /// <param name="parentId"></param>
       public static void ConvertOrganize(List<BaseOrganize> organizeList, List<BaseTree> organizeTreeList, string parentId, string departmentId)
       {
           foreach (var organize in organizeList.Where(o => o.ParentId == parentId))
           {
               
               BaseTree organizeTree = new BaseTree()
               {
                   id = organize.Id,
                   pid = organize.ParentId,
                   name = organize.Name,
                   pname = organize.ParentName,
                   type = organize.Type,
                   font = new font() { color = "#08c" },
                   code = organize.Code,
                   Amount = organize.Amount,
                   isleaf = organize.isleaf,
                   SortCode = organize.SortCode
               };
               if (organize.Id == departmentId)
               {
                   organizeTree.isChecked = true;
               }
               else
               {
                   organizeTree.isChecked = false;
               }
               if (organize.Type == 2 || organize.Type == 1)
               {
                   organizeTree.nocheck = true;
               }
               else
               {
                   organizeTree.nocheck = false;
               }
               organizeTree.children = new List<BaseTree>();
               organizeTreeList.Add(organizeTree);
               ConvertOrganize(organizeList, organizeTree.children, organizeTree.id, departmentId);
           }
       }

       /// <summary>
       /// 获取组织机构树首页展示
       /// </summary>
       /// <param name="where"></param>
       /// <param name="orderBy"></param>
       /// <returns></returns>
       public string GetOrganizeListWhere(string where, string orderBy)
       {
           List<BaseTree> organizeTreeList = new List<BaseTree>();
           ConvertOrganize(baseOrganizeDal.GetListWhere(null, null), organizeTreeList, "0");
           return JsonConvert.SerializeObject(organizeTreeList);
       }



       /// <summary>
       /// 递归转换组织机构
       /// </summary>
       /// <param name="organizeList"></param>
       /// <param name="organizeTreeList"></param>
       /// <param name="parentId"></param>
       public static void ConvertOrganize(List<BaseOrganize> organizeList, List<BaseTree> organizeTreeList, string parentId)
       {
           foreach (var organize in organizeList.Where(o => o.ParentId == parentId))
           {

               BaseTree organizeTree = new BaseTree()
               {
                   id = organize.Id,
                   pid = organize.ParentId,
                   pname = organize.ParentName,
                   type = organize.Type,
                   target = "rightFrame",
                   url = "/SysManage/BaseOrganize/Update?id=" + organize.Id,
                   font = new font() { color = "#08c" },
                   code = organize.Code,
                   Amount = organize.Amount,
                   isleaf = organize.isleaf,
                   SortCode = organize.SortCode
               };
               if (organize.Enabled != 1)
               {
                   organizeTree.name = "<span style='text-decoration: line-through;'>" + organize.Name + "</span>";
               }
               else
               {
                   organizeTree.name = organize.Name;
               }
               organizeTree.children = new List<BaseTree>();
               organizeTreeList.Add(organizeTree);
               ConvertOrganize(organizeList, organizeTree.children, organizeTree.id);
           }
       }

          /// <summary>
        /// 获取组织机构角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
       public List<BaseRole> GetOrganizeRoleListWhere(string departmentId)
       {
           return baseOrganizeDal.GetOrganizeRoleListWhere(departmentId);
       }

           /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
       public bool SaveAllotRole(string departmentId, string roleIds)
       {
           return baseOrganizeDal.SaveAllotRole(departmentId, roleIds);
       }
    }
}
